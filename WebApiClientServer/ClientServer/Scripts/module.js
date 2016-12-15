angular.module('Module', [])
    .service('Connector', [
        '$http',
        function ($http) {
            return {
                getAll: function () {
                    return $http.get('/api/Item');
                },
                Save: function (data) {
                    return $http.post('/api/Item', data);
                },
                Find: function (id) {
                    return $http.get('/api/Item/' + id);
                },
                Delete: function (id) {
                    return $http.delete('/api/Item/' + id);
                },
                Change: function (id, data) {
                    return $http.put('/api/Item/' + id, data);
                },
                Login: function (data) {
                    return $http.post('/Home/Login', data);
                },
                Logout: function () {
                    return $http.get('/Home/Logout');
                }
            }
        }

    ])
    .controller('Controller', [
    '$scope',
    '$http',
    'Connector',
    function ($scope, $http, Connector) {
        $scope.Find = function () {
            Connector.Find($scope.id).then(function (response) {
                $scope.caption = response.data.Caption;
                $scope.text = response.data.Text;
                $scope.author = response.data.Author;
            });
        };
        $scope.Delete = function () {
            Connector.Delete($scope.id).then(function () {
                $scope.update()
            });
        };        
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
                Id: $scope.id,
                Caption: $scope.caption,
                Text: $scope.text,
                Author: $scope.author
            }).then(function () { $scope.update() });
        };
        $scope.Change = function () {
            Connector.Change($scope.id, {
                Id: $scope.id,
                Caption: $scope.caption,
                Text: $scope.text,
                Author: $scope.author
            }).then(function () { $scope.update() });
        };
    }])
.controller('loger', [
    '$scope',
    '$window',
    'Connector',
    function ($scope, $window, Connector) {
        $scope.Login = function () {
            Connector.Login({ login: $scope.login, password: $scope.password }).then(function () { $window.location.href = '/Home/Index' });
        };
        $scope.Logout = function () {
            Connector.Logout().then(function () { $window.location.href = '/Home/Login' });
        };
    }]);