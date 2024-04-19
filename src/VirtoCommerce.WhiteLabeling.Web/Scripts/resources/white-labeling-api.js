angular.module('WhiteLabeling')
    .factory('WhiteLabeling.webApi', ['$resource', function ($resource) {
        return $resource('api/white-labeling');
    }]);
