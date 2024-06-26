    app.controller('LoginController', ['$scope', '$http', function ($scope, $http) {
        $scope.loginData = {};
        $scope.currentUser = null;
        $scope.showPassword = false;

        $scope.login = function () {
            $http.post('/api/Login/login', $scope.loginData)
                .then(function (response) {
                    var token = response.data.token;
                    sessionStorage.setItem('authToken', token);
                    $scope.getCurrentUser();
                }, function (error) {
                    console.error('Erro ao tentar logar:', error);
                });
        };

        $scope.logout = function () {
            $http.post('/api/Login/logout')
                .then(function () {
                    sessionStorage.removeItem('authToken');
                    $scope.currentUser = null;
                }, function (error) {
                    console.error('Erro ao tentar deslogar:', error);
                });
        };

        $scope.getCurrentUser = function () {
            var token = sessionStorage.getItem('authToken');
            if (token) {
                var payload = JSON.parse(atob(token.split('.')[1]));
                $scope.currentUser = payload.unique_name;
            }
        };

        $scope.getCurrentUser();

        $scope.togglePasswordVisibility = function () {
            $scope.showPassword = !$scope.showPassword;
        };
    }]);
