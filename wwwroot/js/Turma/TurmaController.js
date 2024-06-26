    app.controller("TurmaController", function ($scope, $http) {
        $scope.turma = {};
        $scope.novaTurma = {};
        $scope.turmas = [];
        $scope.professores = []; // Array para armazenar os professores

        // Função para carregar a lista de professores
        $scope.carregarProfessores = function () {
            $http.get("/api/Professor/Listar")
                .then(function (response) {
                    $scope.professores = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Função para obter detalhes de uma turma específica
        $scope.getDetails = function (id) {
            $http.get("/api/Turma/Details?id=" + id)
                .then(function (response) {
                    $scope.turma = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Função para criar uma nova turma
        $scope.criarTurma = function () {
            $http.post("/api/Turma/Create", $scope.novaTurma)
                .then(function (response) {
                    $scope.novaTurma = {}; // Limpar o modelo após a criação bem-sucedida
                    $scope.getListar(); // Recarregar a lista de turmas após a criação bem-sucedida
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Função para carregar a lista de todas as turmas
        $scope.getListar = function () {
            $http.get("/api/Turma/Listar")
                .then(function (response) {
                    $scope.turmas = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Chamada inicial para carregar a lista de professores
        $scope.carregarProfessores();
        // Chamada inicial para carregar a lista de turmas
        $scope.getListar();
    });