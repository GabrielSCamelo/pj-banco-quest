    app.controller("AlunoTurmaController", function ($scope, $http) {
        $scope.alunoTurma = {};

        // Função para carregar a lista de turmas
        $scope.carregarTurmas = function () {
            $http.get("/api/Turma/Listar")
                .then(function (response) {
                    $scope.turmas = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Função para carregar a lista de alunos
        $scope.carregarAlunos = function () {
            $http.get("/api/Aluno/Listar")
                .then(function (response) {
                    $scope.alunos = response.data;
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Função para associar aluno a turma
        $scope.associarAlunoTurma = function () {
            $http.post("/api/AlunoTurma/AssociarAlunoTurma", $scope.alunoTurma)
                .then(function (response) {
                    console.log(response.data);
                    // Limpar formulário ou tomar outras ações necessárias após o sucesso
                })
                .catch(function (error) {
                    console.log(error);
                });
        };

        // Chamadas iniciais para carregar turmas e alunos
        $scope.carregarTurmas();
        $scope.carregarAlunos();
    });