using FluentValidation;
using static VirtoCommerce.WhiteLabeling.Core.ModuleConstants;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Validators;

public class OrganizationLogoUploadValidator : AbstractValidator<OrganizationLogoUploadContext>
{
    public OrganizationLogoUploadValidator()
    {
        RuleFor(x => x.Logo)
            .NotNull()
            .WithMessage("Logo is not uploaded.");

        RuleFor(x => x.Logo.Scope)
            .Equal(OrganizationLogoUploadScope)
            .WithMessage("Logo uploaded with invalid scope.")
            .When(x => x.Logo != null);

        RuleFor(x => x)
            .Custom((request, context) =>
            {
                var logo = request.Logo;
                if (!string.IsNullOrEmpty(logo.OwnerEntityId) || !string.IsNullOrEmpty(logo.OwnerEntityType))
                {
                    context.AddFailure("File is already attached.");
                }
            })
            .When(x => x.Logo != null);
    }
}
