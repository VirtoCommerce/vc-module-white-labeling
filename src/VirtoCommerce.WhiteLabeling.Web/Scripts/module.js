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
            widgetService.registerWidget(whiteLabelingWidget, 'storeDetail');

            var whiteLabelingLogoWidget = {
                controller: 'WhiteLabeling.whiteLabelingLogoWidgetController',
                template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/widgets/whiteLabelingLogoWidget.html'
            };
            widgetService.registerWidget(whiteLabelingLogoWidget, 'whiteLabelingLogos');

            var whiteLabelingFaviconWidget = {
                controller: 'WhiteLabeling.whiteLabelingFaviconWidgetController',
                template: 'Modules/$(VirtoCommerce.WhiteLabeling)/Scripts/widgets/whiteLabelingFaviconWidget.html'
            };
            widgetService.registerWidget(whiteLabelingFaviconWidget, 'whiteLabelingLogos');
        }
    ]);
