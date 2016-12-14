angular.module('Module', [])
    .service('Connector', [
        '$http',
        function ($http) {
            return {
                getAll: function () {
                    return $http.get('/api/Items');
                },
                Save: function (data) {
                    return $http.post('/api/Items', data);
                }
            }
        }
    ])
    .controller('Controller', [
    '$scope',
    '$http',
    'Connector',
    function ($scope, $http, Connector) {
        $scope.update = function () {
            Connector.getAll().then(function (responce) {
                $scope.items = [];

                responce.data.forEach(function (item, i, array) {
                    $scope.items.push({
                        caption: item.Caption,
                        text: item.Text,
                        author: item.Author
                    });
                });
            })
        };
        $scope.update();
        $scope.AddItem = function () {
            Connector.Save({
                Caption: $scope.caption,
                Text: $scope.text,
                Author: $scope.author,
                Id: 0
            }).then(function (responce){$scope.update()});
        };

    }]);