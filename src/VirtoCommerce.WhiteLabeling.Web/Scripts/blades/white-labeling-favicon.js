angular.module('WhiteLabeling')
    .controller('WhiteLabeling.whiteLabelingFaviconController', ['$scope', 'FileUploader', 'platformWebApp.bladeNavigationService', 'platformWebApp.dialogService', 'WhiteLabeling.service',
        function ($scope, FileUploader, bladeNavigationService, dialogService, whitelabelingService) {
            const blade = $scope.blade;
            blade.title = 'white-labeling.blades.white-labeling-favicon.title';
            blade.updatePermission = 'WhiteLabeling:update';

            if (!$scope.faviconUploader) {
                const faviconUploader = $scope.faviconUploader = new FileUploader({
                    scope: $scope,
                    headers: { Accept: 'application/json' },
                    autoUpload: true,
                    removeAfterUpload: true,
                    filters: [{
                        name: 'imageFilter',
                        fn: function (item) {
                            const approval = /^.*\.(png|jpg|jpeg|webp)$/.test(item.name);
                            if (!approval) {
                                const dialog = {
                                    title: 'white-labeling.dialogs.white-labeling-favicon-upload-filter.title',
                                    message: 'white-labeling.dialogs.white-labeling-favicon-upload-filter.message',
                                }
                                dialogService.showErrorDialog(dialog);
                            }
                            return approval;
                        }
                    }]
                });

                faviconUploader.url = 'api/assets?folderUrl=customization/favicons';

                faviconUploader.onAfterAddingFile = function (item) {
                    const fileExtension = '.' + item.file.name.split('.').pop();
                    const entityId = whitelabelingService.getEntityId(blade.currentEntity);
                    item.file.name = `favicon_${entityId}_${Date.now().toString()}${fileExtension}`;
                };

                faviconUploader.onSuccessItem = function (_, uploadedImages) {
                    blade.currentEntity.faviconUrl = uploadedImages[0].url;
                };

                faviconUploader.onErrorItem = function (element, response, status, _) {
                    bladeNavigationService.setError(`${element._file.name} failed: ${response.message ? response.message : status}`, blade);
                };
            }

            let formScope;
            $scope.setForm = function (form) {
                formScope = form;
            }

            $scope.browseFiles = function (id) {
                window.document.querySelector(`#${id}`).click()
            }

            function isDirty() {
                return !angular.equals(blade.currentEntity, blade.origEntity) && blade.hasUpdatePermission();
            }

            function canSave() {
                return isDirty() && formScope && formScope.$valid;
            }

            blade.saveChanges = function () {
                blade.parentBlade.currentEntity.faviconUrl = blade.currentEntity.faviconUrl;

                angular.copy(blade.currentEntity, blade.origEntity);

                $scope.bladeClose();
            };

            blade.refresh = function () {
                blade.origEntity = blade.currentEntity;
                blade.currentEntity = angular.copy(blade.currentEntity);

                blade.isLoading = false;
            }

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
                        blade.currentEntity.faviconUrl = null;
                    },
                    canExecuteMethod: function () {
                        return blade.currentEntity.faviconUrl;
                    }
                }
            ];

            // actions on load
            blade.refresh();
        }]);
