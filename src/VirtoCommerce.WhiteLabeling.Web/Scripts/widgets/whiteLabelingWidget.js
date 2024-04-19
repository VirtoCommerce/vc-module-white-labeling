angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
    
    $scope.openBlade = function () {
        var newBlade = {
            id: "whiteLabelingBlade",
            title: $scope.blade.title,
            organization: $scope.blade.currentEntity,
            subtitle: 'customer.widgets.customer-accounts-list.blade-subtitle',
            controller: 'WhiteLabeling.organizationLabelingController',
            template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/blades/organization-labeling.html'
        };
        bladeNavigationService.showBlade(newBlade, $scope.blade);
    };
}]);
