var app = angular.module('colinaApp', ['ngRoute', 'ngMaterial']);

app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'views/language-selection.html',
        controller: 'StartController',
        controllerAs: 'ctrl'
    }).when('/language/:lang', {
        templateUrl: 'views/main.html',
        controller: 'MainController',
        controllerAs: 'ctrl'
    }).otherwise('/');
}]);
