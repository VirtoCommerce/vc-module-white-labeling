angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingFaviconWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {

    $scope.openBlade = function () {
        var newBlade = {
            id: "whiteLabelingFavicon",
            currentEntity: $scope.blade.currentEntity,
            controller: 'WhiteLabeling.whiteLabelingFaviconController',
            template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/blades/white-labeling-favicon.html'
        };
        bladeNavigationService.showBlade(newBlade, $scope.blade);
    };
}]);
