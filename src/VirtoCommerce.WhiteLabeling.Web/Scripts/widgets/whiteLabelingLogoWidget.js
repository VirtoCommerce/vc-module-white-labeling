angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingLogoWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {

    $scope.openBlade = function () {
        var newBlade = {
            id: "whiteLabelingLogo",
            currentEntity: $scope.blade.currentEntity,
            controller: 'WhiteLabeling.whiteLabelingLogoController',
            template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/blades/white-labeling-logo.html'
        };
        bladeNavigationService.showBlade(newBlade, $scope.blade);
    };
}]);
