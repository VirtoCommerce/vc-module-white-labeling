using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;

namespace VirtoCommerce.WhiteLabeling.Data.Validation;


public class WhiteLabelingSettingValidator : AbstractValidator<WhiteLabelingSetting>
{
    public WhiteLabelingSettingValidator(IWhiteLabelingSettingService service, IWhiteLabelingSettingSearchService searchService)
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        // Either StoreId or OrganizationId must be set
        RuleFor(r => r)
           .Custom((request, context) =>
           {
               if (!request.StoreId.IsNullOrEmpty() && !request.OrganizationId.IsNullOrEmpty())
               {
                   context.AddFailure(new ValidationFailure(nameof(WhiteLabelingSetting.StoreId), "store-and-organization-set"));
               }

               if (request.StoreId.IsNullOrEmpty() && request.OrganizationId.IsNullOrEmpty())
               {
                   context.AddFailure(new ValidationFailure(nameof(WhiteLabelingSetting.StoreId), "store-or-organization-must-be-set"));
               }
           });

        RuleFor(r => r)
           .CustomAsync(async (request, context, _) =>
           {
               WhiteLabelingSetting result = null;
               if (!request.Id.IsNullOrEmpty())
               {
                   result = await service.GetNoCloneAsync(request.Id);
               }

               if (result != null)
               {
                   // Can't change StoreId or OrganizationId for the existing white labeling
                   if (result.StoreId != request.StoreId || result.OrganizationId != request.OrganizationId)
                   {
                       var propertyName = GetPropertyName(request);
                       context.AddFailure(new ValidationFailure(propertyName, "store-or-organization-changed"));
                   }
               }
               else
               {
                   // Can't have duplicate StoreId or OrganizationId for a new white labeling
                   if (await HasDuplicate(searchService, request))
                   {
                       var propertyName = GetPropertyName(request);
                       context.AddFailure(new ValidationFailure(propertyName, "duplicate-store-or-organization"));
                   }
               }
           });
    }

    private static async Task<bool> HasDuplicate(IWhiteLabelingSettingSearchService searchService, WhiteLabelingSetting request)
    {
        var criteria = AbstractTypeFactory<WhiteLabelingSettingSearchCriteria>.TryCreateInstance();
        criteria.OrganizationId = request.OrganizationId;
        criteria.StoreId = request.StoreId;
        criteria.Take = 0;

        var searchResult = await searchService.SearchNoCloneAsync(criteria);
        return searchResult.TotalCount > 0;
    }

    private static string GetPropertyName(WhiteLabelingSetting request)
    {
        return !request.StoreId.IsNullOrEmpty() ? nameof(WhiteLabelingSetting.StoreId) : nameof(WhiteLabelingSetting.OrganizationId);
    }
}
