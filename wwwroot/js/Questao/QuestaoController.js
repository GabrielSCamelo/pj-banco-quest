app.controller("QuestaoController", function ($scope, $http) {
    $scope.questao = {};
    $scope.questaoSelecionada = null;
    $scope.questoes = JSON.parse(localStorage.getItem('questoes')) || [];
    $scope.isLoading = false; // Variável para controlar o estado de carregamento
    $scope.isEditing = false; // Variável para controlar o modo de edição

    $scope.adicionarQuestaoTemp = function () {
        $scope.questoes.push(angular.copy($scope.questao));
        localStorage.setItem('questoes', JSON.stringify($scope.questoes));
        $scope.questao = {}; // Limpar o formulário após adicionar a questão
    };

    $scope.listarQuestoes = function () {
        $http.get("/api/Questao/Listar")
            .then(function (response) {
                $scope.questoes = response.data;
            })
            .catch(function (error) {
                console.log(error);
            });
    };

    $scope.selecionarQuestao = function (questao) {
        $scope.questaoSelecionada = angular.copy(questao); // Usar cópia para evitar edição direta
        $scope.isEditing = false; // Inicia sem edição
    };

    $scope.iniciarEdicao = function () {
        $scope.isEditing = true; // Ativar modo de edição
    };

    $scope.cancelarEdicao = function () {
        $scope.questaoSelecionada = null; // Cancelar edição e limpar seleção
        $scope.isEditing = false; // Desativar modo de edição
    };

    $scope.atualizarQuestao = function () {
        var index = $scope.questoes.findIndex(q => q.titulo === $scope.questaoSelecionada.titulo);
        if (index !== -1) {
            $scope.questoes[index] = angular.copy($scope.questaoSelecionada);
            localStorage.setItem('questoes', JSON.stringify($scope.questoes));
            $scope.questaoSelecionada = null; // Limpar a questão selecionada após a atualização
            $scope.isEditing = false; // Desativar modo de edição
        }
    };

    $scope.excluirQuestao = function () {
        var index = $scope.questoes.findIndex(q => q.titulo === $scope.questaoSelecionada.titulo);
        if (index !== -1) {
            $scope.questoes.splice(index, 1);
            localStorage.setItem('questoes', JSON.stringify($scope.questoes));
            $scope.questaoSelecionada = null; // Limpar a questão selecionada após a exclusão
        }
    };

    $scope.salvarTodasQuestoes = function () {
        $scope.isLoading = true; // Ativar indicador de carregamento

        $http.post("/api/Questao/SalvarTodas", $scope.questoes)
            .then(function (response) {
                $scope.questoes = []; // Limpar a lista de questões após salvar
                localStorage.removeItem('questoes'); // Limpar o localStorage
            })
            .catch(function (error) {
                console.log(error);
            })
            .finally(function () {
                $scope.isLoading = false; // Desativar indicador de carregamento
            });
    };

    $scope.listarQuestoes();
});