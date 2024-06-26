    app.controller('ProfessorController', function ($scope, $http) {
        $scope.professores = [];
        $scope.professorSelecionado = null;
        $scope.novoProfessor = {};

        $scope.listarProfessores = function () {
            $http.get('/api/Professor/Listar')
                .then(function (response) {
                    $scope.professores = response.data;
                }, function (error) {
                    console.error('Erro ao listar professores:', error);
                });
        };

        $scope.detalhesProfessor = function (id) {
            $http.get('/api/Professor/Details?id=' + id) 
                .then(function (response) {
                    $scope.professorSelecionado = response.data;
                }, function (error) {
                    console.error('Erro ao obter detalhes do professor:', error);
                });
        };

        $scope.criarProfessor = function () {
            $http.post('/api/Professor/Create', $scope.novoProfessor)
                .then(function (response) {
                    $scope.professores.push(response.data);
                    $scope.novoProfessor = {};
                    alert('Professor criado com sucesso!');
                }, function (error) {
                    console.error('Erro ao criar professor:', error);
                    alert('Erro ao criar professor: ' + error.data);
                });
        };
        $scope.listarProfessores();
    });

