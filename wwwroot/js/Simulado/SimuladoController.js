app.controller('SimuladoController', function ($scope, $http, $window) {
    $scope.simulado = {};
    $scope.novoSimulado = {};
    $scope.simulados = [];
    $scope.disciplinas = [];
    $scope.turmas = [];
    $scope.isProfessor = false;

    // Verificar se o usuário é professor
    $scope.verificarUsuario = function () {
        $http.get("/api/Login/TipoUsuario")
            .then(function (response) {
                $scope.isProfessor = response.data.tipo === "Professor";
                if ($scope.isProfessor) {
                    $scope.carregarTurmas();
                }
                $scope.carregarDisciplinas();
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.carregarDisciplinas = function () {
        $http.get("/api/Disciplina/Listar")
            .then(function (response) {
                $scope.disciplinas = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.carregarTurmas = function () {
        $http.get("/api/Turma/Listar")
            .then(function (response) {
                $scope.turmas = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.getDetails = function (id) {
        $http.get("/api/Simulado/Details?id=" + id)
            .then(function (response) {
                $scope.simulado = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.criarSimulado = function () {
        // Formatar a lista de disciplinas como array de inteiros
        $scope.novoSimulado.disciplinas = $scope.novoSimulado.disciplinas.split(',').map(Number);
        // Adicionar turmas selecionadas se o usuário for professor
        if ($scope.isProfessor) {
            $scope.novoSimulado.turmas = $scope.novoSimulado.turmas.map(Number);
        }

        $http.post("/api/Simulado/Create", $scope.novoSimulado)
            .then(function (response) {
                $scope.novoSimulado = {}; // Limpar o modelo após a criação bem-sucedida
                $scope.getListar(); // Recarregar a lista de simulados após a criação bem-sucedida
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.getListar = function () {
        $http.get("/api/Simulado/Listar")
            .then(function (response) {
                $scope.simulados = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    // Inicializar o controller
    $scope.verificarUsuario();
    $scope.getListar();
});
