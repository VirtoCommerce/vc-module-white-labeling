// Call this to register your module to main application
var moduleName = 'WhiteLabeling';

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .run(['platformWebApp.widgetService',
        function (widgetService) {
            var whiteLabelingWidget = {
                controller: 'WhiteLabeling.whiteLabelingWidgetController',
                template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/widgets/whiteLabelingWidget.html',
                isVisible: function (blade) { return !blade.isNew; }
            };

            widgetService.registerWidget(whiteLabelingWidget, 'organizationDetail2');
        }
    ]);
