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

        // Can't have duplicate StoreId or OrganizationId
        RuleFor(r => r)
           .CustomAsync(async (request, context, _) =>
           {
               var criteria = new WhiteLabelingSettingSearchCriteria()
               {
                   OrganizationId = request.OrganizationId,
                   StoreId = request.StoreId,
                   Take = 0,
               };

               var searchResult = await searchService.SearchNoCloneAsync(criteria);

               if (searchResult.TotalCount > 0)
               {
                   var propertyName = GetPropertyName(request);
                   context.AddFailure(new ValidationFailure(propertyName, "duplicate-store-or-organization"));
               }
           })
           .When(x => x.Id.IsNullOrEmpty());

        // Can't change StoreId or OrganizationId
        RuleFor(r => r)
           .CustomAsync(async (request, context, _) =>
           {
               var result = await service.GetNoCloneAsync(request.Id);

               if (result != null &&
                 (result.StoreId != request.StoreId ||
                 result.OrganizationId != request.OrganizationId))
               {
                   var propertyName = GetPropertyName(request);
                   context.AddFailure(new ValidationFailure(propertyName, "store-or-organization-changed"));
               }
           })
           .When(x => !x.Id.IsNullOrEmpty());
    }

    private static string GetPropertyName(WhiteLabelingSetting request)
    {
        return !request.StoreId.IsNullOrEmpty() ? nameof(WhiteLabelingSetting.StoreId) : nameof(WhiteLabelingSetting.OrganizationId);
    }
}
