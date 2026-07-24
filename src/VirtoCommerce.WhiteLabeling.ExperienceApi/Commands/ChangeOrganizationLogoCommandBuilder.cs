using System;
using System.Threading.Tasks;
using GraphQL;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.FileExperienceApi.Core.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Authorization;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Models;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Schemas;
using VirtoCommerce.Xapi.Core.BaseQueries;
using static VirtoCommerce.FileExperienceApi.Core.ModuleConstants.Security.Permissions;
using static VirtoCommerce.WhiteLabeling.Core.ModuleConstants;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Commands;

public class ChangeOrganizationLogoCommandBuilder : CommandBuilder<ChangeOrganizationLogoCommand, ChangeOrganizationLogoResult, InputChangeOrganizationLogoCommandType, ChangeOrganizationLogoResultType>
{
    protected override string Name => "ChangeOrganizationLogo";

    public ChangeOrganizationLogoCommandBuilder(IAuthorizationService authorizationService)
        : base(authorizationService)
    {
    }

    [Obsolete("Use the constructor without IMediator. The mediator is resolved from context.RequestServices per request.", DiagnosticId = "VC0015", UrlFormat = "https://docs.virtocommerce.org/products/products-virto3-versions")]
    public ChangeOrganizationLogoCommandBuilder(
        IMediator mediator,
        IAuthorizationService authorizationService)
        : this(authorizationService)
    {
    }

    protected override async Task BeforeMediatorSend(IResolveFieldContext<object> context, ChangeOrganizationLogoCommand request)
    {
        await base.BeforeMediatorSend(context, request);

        var organizationFileResource = new File
        {
            OwnerEntityId = request.OrganizationId,
            OwnerEntityType = nameof(Organization),
            Scope = OrganizationLogoUploadScope,
        };

        await Authorize(context, organizationFileResource, new OrganizationLogoAuthorizationRequirement(Update));
    }
}
