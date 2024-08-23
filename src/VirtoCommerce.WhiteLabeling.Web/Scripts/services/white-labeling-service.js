angular.module('WhiteLabeling')
    .factory('WhiteLabeling.service', [function () {
        function getEntityId(entity) {
            var entityId = entity.id;

            if (entity.organizationId) {
                entityId = entity.organizationId;
            } else if (entity.storeId) {
                entityId = entity.storeId;
            }

            return entityId;
        }

        return {
            getEntityId: getEntityId
        }
    }]);
