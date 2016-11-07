var app = angular.module('colinaApp');

app.controller('mainController', ['colinaService', function (colinaService) {
    var controller = this;
    controller.language = 'pt-BR';
    controller.runningQuery = false;

    colinaService.setLanguage(controller.language);

    controller.palette = colinaService.getPalette();
}]);