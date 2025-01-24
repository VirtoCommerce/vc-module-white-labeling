using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.FileExperienceApi.Core.Models;
using VirtoCommerce.FileExperienceApi.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Validators;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Commands;

public class ChangeOrganizationLogoCommandHandler : IRequestHandler<ChangeOrganizationLogoCommand, ChangeOrganizationLogoResult>
{
    private readonly IFileUploadService _fileUploadService;
    private readonly IWhiteLabelingSettingService _whiteLabelingSettingService;
    private readonly IWhiteLabelingSettingSearchService _whiteLabelingSettingSearchService;

    private const string _fileUrlPrefix = "/api/files/";

    public ChangeOrganizationLogoCommandHandler(
        IFileUploadService fileUploadService,
        IWhiteLabelingSettingService whiteLabelingSettingService,
        IWhiteLabelingSettingSearchService whiteLabelingSettingSearchService)
    {
        _fileUploadService = fileUploadService;
        _whiteLabelingSettingSearchService = whiteLabelingSettingSearchService;
        _whiteLabelingSettingService = whiteLabelingSettingService;
    }

    public virtual async Task<ChangeOrganizationLogoResult> Handle(ChangeOrganizationLogoCommand request, CancellationToken cancellationToken)
    {
        var whiteLabelingSetting = await GetWhiteLabelingSettingAsync(request.OrganizationId);
        if (whiteLabelingSetting == null)
        {
            // create white labeling for this organization
            var newWhiteLabelingSetting = AbstractTypeFactory<WhiteLabelingSetting>.TryCreateInstance();
            newWhiteLabelingSetting.OrganizationId = request.OrganizationId;
            newWhiteLabelingSetting.IsEnabled = true;
        }

        var logoUrlFile = await UpdateLogoUrlFile(request);
        if (logoUrlFile.Item1 == null)
        {
            return new ChangeOrganizationLogoResult
            {
                ErrorMessage = logoUrlFile.Item2?.FirstOrDefault()?.ErrorMessage
            };
        }

        whiteLabelingSetting.LogoUrl = request.LogoUrl;

        await _whiteLabelingSettingService.SaveChangesAsync([whiteLabelingSetting]);

        return new ChangeOrganizationLogoResult
        {
            IsSuccess = true
        };
    }

    private async Task<WhiteLabelingSetting> GetWhiteLabelingSettingAsync(string organizationId)
    {
        var searchCriteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();

        searchCriteria.OrganizationId = organizationId;
        searchCriteria.IsEnabled = true;
        searchCriteria.Take = 1;

        var searchResult = await _whiteLabelingSettingSearchService.SearchAsync(searchCriteria);

        return searchResult.Results?.FirstOrDefault();
    }

    protected virtual async Task<(File, List<ValidationFailure>)> UpdateLogoUrlFile(ChangeOrganizationLogoCommand request)
    {
        var file = await _fileUploadService.GetByIdAsync(GetFileId(request.LogoUrl));

        var validationResult = AbstractTypeFactory<OrganizationLogoUploadValidator>.TryCreateInstance()
            .Validate(new OrganizationLogoUploadContext
            {
                Logo = file,
                OrganizationId = request.OrganizationId
            });

        if (!validationResult.IsValid)
        {
            return (null, validationResult.Errors);
        }

        file.OwnerEntityId = request.OrganizationId;
        file.OwnerEntityType = nameof(Organization);

        await _fileUploadService.SaveChangesAsync([file]);

        return (file, null);
    }

    protected static string GetFileId(string url)
    {
        return url != null && url.StartsWith(_fileUrlPrefix)
            ? url[_fileUrlPrefix.Length..]
            : null;
    }
}
