angular.module('WhiteLabeling')
    .controller('WhiteLabeling.organizationLabelingController', ['$scope', 'WhiteLabeling.webApi', 'platformWebApp.bladeNavigationService', function ($scope, api, bladeNavigationService) {
        let blade = $scope.blade;
        blade.title = 'white-labeling.blades.white-labeling-detail.title';
        blade.updatePermission = 'WhiteLabeling:update';

        blade.refresh = function () {
            blade.isLoading = true;

            api.getByOrganization({ organizationId: blade.organization.id }, function (data) {

                if (data && data.id) {
                    blade.currentEntity = angular.copy(data);
                    blade.originalEntity = data;
                }
                else {
                    blade.isNew = true;
                    blade.originalEntity = {
                        isEnabled: false,
                        organizationId: blade.organization.id
                    };
                    blade.currentEntity = angular.copy(blade.originalEntity);
                }

                blade.isLoading = false;
            });
        };

        blade.saveChanges = function () {
            blade.isLoading = true;

            if (blade.isNew) {
                api.createSetting(blade.currentEntity,
                    function () {
                        $scope.bladeClose();
                    });
            } else {
                api.updateSetting(blade.currentEntity,
                    function () {
                        blade.refresh();
                    });
            }
        };

        $scope.setForm = function (form) {
            $scope.formScope = form;
        }

        function canSave() {
            return isDirty() && $scope.formScope && $scope.formScope.$valid;
        }

        function isDirty() {
            return !angular.equals(blade.currentEntity, blade.originalEntity) && blade.hasUpdatePermission();
        }

        blade.openContent = function () {
            var newBlade = {
                id: 'content',
                title: 'content.blades.content-main.title',
                subtitle: 'content.blades.content-main.subtitle',
                controller: 'virtoCommerce.contentModule.contentMainController',
                template: 'Modules/$(VirtoCommerce.Content)/Scripts/blades/content-main.tpl.html',
                isClosingDisabled: false
            };
            bladeNavigationService.showBlade(newBlade, blade);
        }

        blade.toolbarCommands = [
            {
                name: "platform.commands.save",
                icon: 'fas fa-save',
                executeMethod: blade.saveChanges,
                canExecuteMethod: canSave,
                permission: blade.updatePermission
            }];

        blade.refresh();
    }]);
