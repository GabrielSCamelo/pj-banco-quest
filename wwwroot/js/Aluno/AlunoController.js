    app.controller("AlunoController", function ($scope, $http) {
        $scope.aluno = {};
        $scope.novoAluno = {};
        $scope.alunos = [];

        $scope.getDetails = function (id) {
            $http.get("/api/Aluno/Details?id=" + id)
                .then(function (response) {
                    $scope.aluno = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        $scope.criarAluno = function () {
            $http.post("/api/Aluno/Create", $scope.novoAluno)
                .then(function (response) {
                    $scope.novoAluno = {}; // Limpar o modelo após a criação bem-sucedida
                    $scope.getListar(); // Recarregar a lista de alunos após a criação bem-sucedida
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        $scope.getListar = function () {
            $http.get("/api/Aluno/Listar")
                .then(function (response) {
                    $scope.alunos = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Chamada inicial para listar todos os alunos
        $scope.getListar();
    });
