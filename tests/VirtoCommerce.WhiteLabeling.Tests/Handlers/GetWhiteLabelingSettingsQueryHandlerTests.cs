using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using VirtoCommerce.CustomerModule.Core.Model;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.WhiteLabeling.Core.Models;
using VirtoCommerce.WhiteLabeling.Core.Services;
using VirtoCommerce.WhiteLabeling.ExperienceApi.Queries;
using VirtoCommerce.Xapi.Core.Services;
using Xunit;

namespace VirtoCommerce.WhiteLabeling.Tests.Handlers
{
    [Trait("Category", "Unit")]
    public class GetWhiteLabelingSettingsQueryHandlerTests
    {
        private readonly Mock<IWhiteLabelingSettingSearchService> _searchServiceMock = new();
        private readonly Mock<IMemberService> _memberServiceMock = new();
        private readonly Mock<IMediator> _mediatorMock = new();
        private readonly Mock<IStoreDomainResolverService> _storeDomainResolverServiceMock = new();

        [Fact]
        public async Task Handle_FaviconUrlWithoutExtension_DoesNotThrow_AndOmitsMimeType()
        {
            // Arrange — malformed favicon URL with no file extension (e.g. bad data / imported via API)
            SetupOrganizationSetting(new WhiteLabelingSetting
            {
                IsEnabled = true,
                OrganizationId = "org-1",
                FaviconUrl = "/api/files/does-not-exist-QA5519",
            });

            var handler = BuildHandler();
            var query = new GetWhiteLabelingSettingsQuery { OrganizationId = "org-1", StoreId = "store-1" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(5, result.Favicons.Count);
            Assert.All(result.Favicons, favicon => Assert.Null(favicon.Type));

            var favicon16 = result.Favicons.Single(x => x.Sizes == "16x16");
            Assert.Equal("/api/files/does-not-exist-QA5519_16x16", favicon16.Href);
        }

        [Fact]
        public async Task Handle_FaviconUrlWithExtension_GeneratesMimeTypeAndSizedHref()
        {
            // Arrange — regression check: well-formed favicon URL must keep working as before
            SetupOrganizationSetting(new WhiteLabelingSetting
            {
                IsEnabled = true,
                OrganizationId = "org-1",
                FaviconUrl = "https://cdn.example.com/assets/favicon.png",
            });

            var handler = BuildHandler();
            var query = new GetWhiteLabelingSettingsQuery { OrganizationId = "org-1", StoreId = "store-1" };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(5, result.Favicons.Count);
            Assert.All(result.Favicons, favicon => Assert.Equal("image/png", favicon.Type));

            var favicon16 = result.Favicons.Single(x => x.Sizes == "16x16");
            Assert.Equal("https://cdn.example.com/assets/favicon_16x16.png", favicon16.Href);
        }

        private void SetupOrganizationSetting(WhiteLabelingSetting setting)
        {
            _searchServiceMock
                .Setup(x => x.SearchAsync(It.IsAny<WhiteLabelingSettingSearchCriteria>(), It.IsAny<bool>()))
                .ReturnsAsync(new WhiteLabelingSettingSearchResult { Results = [setting], TotalCount = 1 });

            _memberServiceMock
                .Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Member)null);
        }

        private GetWhiteLabelingSettingsQueryHandler BuildHandler() =>
            new(_searchServiceMock.Object, _memberServiceMock.Object, _mediatorMock.Object, _storeDomainResolverServiceMock.Object);
    }
}
