angular.module('WhiteLabeling')
    .controller('WhiteLabeling.organizationLabelingController', ['$scope', 'WhiteLabeling.webApi', function ($scope, api) {
        var blade = $scope.blade;
        blade.title = 'WhiteLabeling';

        blade.refresh = function () {
            api.get(function (data) {
                blade.title = 'WhiteLabeling.blades.hello-world.title';
                blade.data = data.result;
                blade.isLoading = false;
            });
        };

        blade.refresh();
    }]);
