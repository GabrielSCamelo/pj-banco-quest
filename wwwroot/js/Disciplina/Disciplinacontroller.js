    app.controller("DisciplinaController", function ($scope, $http) {
        $scope.disciplinaSelecionada = null;
        $scope.novaDisciplina = {};
        $scope.disciplinas = [];

        $scope.getDetails = function (id) {
            $http.get("/api/Disciplina/Details?id=" + id)
                .then(function (response) {
                    $scope.disciplinaSelecionada = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        $scope.criarDisciplina = function () {
            $http.post("/api/Disciplina/Create", $scope.novaDisciplina)
                .then(function (response) {
                    $scope.disciplinas.push(response.data);
                    $scope.novaDisciplina = {}; // Limpar o modelo após a criação bem-sucedida
                    $scope.getListar(); // Recarregar a lista de disciplinas após a criação bem-sucedida
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        $scope.getListar = function () {
            $http.get("/api/Disciplina/Listar")
                .then(function (response) {
                    $scope.disciplinas = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Chamada inicial para listar todas as disciplinas
        $scope.getListar();
    });