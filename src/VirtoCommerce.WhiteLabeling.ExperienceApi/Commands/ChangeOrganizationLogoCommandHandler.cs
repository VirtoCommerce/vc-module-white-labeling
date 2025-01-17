using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.FileExperienceApi.Core.Models;
using VirtoCommerce.FileExperienceApi.Core.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Commands;
using static VirtoCommerce.WhiteLabeling.Core.ModuleConstants;

namespace VirtoCommerce.FileExperienceApi.Data.Commands;

public class ChangeOrganizationLogoCommandHandler : IRequestHandler<ChangeOrganizationLogoCommand, bool>
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

    public virtual async Task<bool> Handle(ChangeOrganizationLogoCommand request, CancellationToken cancellationToken)
    {
        var whiteLabelingSetting = await GetWhiteLabelingSettingAsync(request.OrganizationId);
        if (whiteLabelingSetting == null)
        {
            return false;
        }

        var logoUrlFile = await UpdateLogoUrlFile(request);
        if (logoUrlFile == null)
        {
            return false;
        }

        whiteLabelingSetting.LogoUrl = request.LogoUrl;

        await _whiteLabelingSettingService.SaveChangesAsync([whiteLabelingSetting]);

        return true;
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

    protected virtual async Task<File> UpdateLogoUrlFile(ChangeOrganizationLogoCommand request)
    {
        var file = await _fileUploadService.GetNoCloneAsync(GetFileId(request.LogoUrl));

        if (file == null ||
            file.Scope != OrganizationLogoUploadScope ||
            !string.IsNullOrEmpty(file.OwnerEntityId) || !string.IsNullOrEmpty(file.OwnerEntityType))
        {
            return null;
        }

        file.OwnerEntityId = request.OrganizationId;
        file.OwnerEntityType = nameof(Organization);

        await _fileUploadService.SaveChangesAsync([file]);

        return file;
    }

    protected static string GetFileId(string url)
    {
        return url != null && url.StartsWith(_fileUrlPrefix)
            ? url[_fileUrlPrefix.Length..]
            : null;
    }
}
