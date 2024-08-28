angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingLogoController', ['$scope', 'FileUploader', 'platformWebApp.bladeNavigationService', 'platformWebApp.dialogService', 'WhiteLabeling.service',
        function ($scope, FileUploader, bladeNavigationService, dialogService, whiteLabelingService) {
            const blade = $scope.blade;
            blade.title = 'white-labeling.blades.white-labeling-logo.title';
            blade.updatePermission = 'WhiteLabeling:update';

            if (!$scope.logoUploader) {
                const logoUploader = $scope.logoUploader = new FileUploader({
                    scope: $scope,
                    headers: { Accept: 'application/json' },
                    autoUpload: true,
                    removeAfterUpload: true,
                    filters: [{
                        name: 'imageFilter',
                        fn: function (item) {
                            const approval = /^.*\.(png|gif|svg)$/.test(item.name);
                            if (!approval) {
                                const dialog = {
                                    title: 'white-labeling.dialogs.white-labeling-icon-upload-filter.title',
                                    message: 'white-labeling.dialogs.white-labeling-icon-upload-filter.message',
                                };
                                dialogService.showErrorDialog(dialog);
                            }
                            return approval;
                        }
                    }]
                });

                logoUploader.url = 'api/assets?folderUrl=customization';

                logoUploader.onAfterAddingFile = function (item) {
                    const fileExtension = '.' + item.file.name.split('.').pop();
                    const entityId = whiteLabelingService.getEntityId(blade.currentEntity);
                    item.file.name = `logo_${entityId}_${Date.now().toString()}${fileExtension}`;
                };

                logoUploader.onSuccessItem = function (_, uploadedImages) {
                    blade.currentEntity.logoUrl = uploadedImages[0].url;
                };

                logoUploader.onErrorItem = function (element, response, status, _) {
                    bladeNavigationService.setError(`${element._file.name} failed: ${response.message ? response.message : status}`, blade);
                };
            }

            if (!$scope.secondaryLogoUploader) {
                const secondaryLogoUploader = $scope.secondaryLogoUploader = new FileUploader({
                    scope: $scope,
                    headers: { Accept: 'application/json' },
                    autoUpload: true,
                    removeAfterUpload: true,
                    filters: [{
                        name: 'imageFilter',
                        fn: function (item) {
                            const approval = /^.*\.(png|gif|svg)$/.test(item.name);
                            if (!approval) {
                                const dialog = {
                                    title: "Filetype error",
                                    message: "Only PNG, GIF or SVG files are allowed.",
                                };
                                dialogService.showErrorDialog(dialog);
                            }
                            return approval;
                        }
                    }]
                });

                secondaryLogoUploader.url = 'api/assets?folderUrl=customization';

                secondaryLogoUploader.onSuccessItem = function (_, uploadedImages) {
                    blade.currentEntity.secondaryLogoUrl = uploadedImages[0].url;
                };

                secondaryLogoUploader.onAfterAddingFile = function (item) {
                    const fileExtension = '.' + item.file.name.split('.').pop();
                    const entityId = whiteLabelingService.getEntityId(blade.currentEntity);
                    item.file.name = `secondary_logo_${entityId}_${Date.now().toString()}${fileExtension}`;
                };

                secondaryLogoUploader.onErrorItem = function (element, response, status, headers) {
                    bladeNavigationService.setError(`${element._file.name} failed: ${response.message ? response.message : status}`, blade);
                };
            }

            let formScope;
            $scope.setForm = function (form) {
                formScope = form;
            };

            $scope.browseFiles = function (id) {
                window.document.querySelector(`#${id}`).click();
            };

            function isDirty() {
                return !angular.equals(blade.currentEntity, blade.origEntity) && blade.hasUpdatePermission();
            }

            function canSave() {
                return isDirty() && formScope && formScope.$valid;
            }

            blade.saveChanges = function () {
                blade.parentBlade.currentEntity.logoUrl = blade.currentEntity.logoUrl;
                blade.parentBlade.currentEntity.secondaryLogoUrl = blade.currentEntity.secondaryLogoUrl;

                angular.copy(blade.currentEntity, blade.origEntity);

                $scope.bladeClose();
            };

            blade.refresh = function () {
                blade.origEntity = blade.currentEntity;
                blade.currentEntity = angular.copy(blade.currentEntity);

                blade.isLoading = false;
            };

            blade.toolbarCommands = [
                {
                    name: "platform.commands.save",
                    icon: 'fas fa-save',
                    executeMethod: blade.saveChanges,
                    canExecuteMethod: canSave
                },
                {
                    name: "platform.commands.reset",
                    icon: 'fa fa-undo',
                    executeMethod: function () {
                        blade.currentEntity = angular.copy(blade.origEntity);
                    },
                    canExecuteMethod: isDirty
                },
                {
                    name: "platform.commands.clear",
                    icon: 'fa fa-eraser',
                    executeMethod: function () {
                        blade.currentEntity.logoUrl = null;
                        blade.currentEntity.secondaryLogoUrl = null;
                    },
                    canExecuteMethod: function () {
                        return blade.currentEntity.logoUrl || blade.currentEntity.secondaryLogoUrl;
                    }
                }
            ];

            // actions on load
            blade.refresh();
        }]);
