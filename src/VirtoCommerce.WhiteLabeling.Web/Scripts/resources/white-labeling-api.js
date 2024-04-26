angular.module('WhiteLabeling')
    .factory('WhiteLabeling.webApi', ['$resource', function ($resource) {
        return $resource('api/white-labeling', {}, {
            getSetting: { method: 'GET', url: 'api/white-labeling/:id' },
            getByOrganization: { method: 'GET', url: 'api/white-labeling/organization/:organizationId' },
            createSetting: { method: 'POST' },
            updateSetting: { method: 'PUT' }
        })
    }]);
