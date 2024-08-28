angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingWidgetController', ['$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {

        $scope.openBlade = function () {
            var newBlade = {
                id: 'whiteLabelingBlade',
                subtitle: $scope.blade.title,
                controller: 'WhiteLabeling.whiteLabelingDetailsController',
                template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/blades/white-labeling-details.html'
            };

            switch ($scope.blade.currentEntity.objectType) {
                case 'VirtoCommerce.CustomerModule.Core.Model.Organization':
                    newBlade.organization = $scope.blade.currentEntity;
                    break;
                case 'VirtoCommerce.StoreModule.Core.Model.Store':
                    newBlade.store = $scope.blade.currentEntity;
                    break;
                default:
                    return;
            }

            bladeNavigationService.showBlade(newBlade, $scope.blade);
        };
    }]);
