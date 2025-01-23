using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Authorization;
using VirtoCommerce.Xapi.Core.BaseQueries;
using static VirtoCommerce.FileExperienceApi.Core.ModuleConstants.Security.Permissions;

namespace VirtoCommerce.WhiteLabeling.ExperienceApi.Commands;

public class ChangeOrganizationLogoCommandBuilder : CommandBuilder<ChangeOrganizationLogoCommand, bool, InputChangeOrganizationLogoCommandType, BooleanGraphType>
{
    protected override string Name => "ChangeOrganizationLogo";

    public ChangeOrganizationLogoCommandBuilder(
        IMediator mediator,
        IAuthorizationService authorizationService)
        : base(mediator, authorizationService)
    {
    }

    protected override async Task BeforeMediatorSend(IResolveFieldContext<object> context, ChangeOrganizationLogoCommand request)
    {
        await base.BeforeMediatorSend(context, request);
        await Authorize(context, request.OrganizationId, new OrganizationLogoAuthorizationRequirement(Update));
    }
}
