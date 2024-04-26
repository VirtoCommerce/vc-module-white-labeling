angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {

    $scope.openBlade = function () {
        var newBlade = {
            id: "whiteLabelingBlade",
            subtitle: $scope.blade.title,
            organization: $scope.blade.currentEntity,
            controller: 'WhiteLabeling.organizationLabelingController',
            template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/blades/organization-labeling.html'
        };
        bladeNavigationService.showBlade(newBlade, $scope.blade);
    };
}]);
