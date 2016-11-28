var app = angular.module('colinaApp');

app.controller('StartController', ['$location', 'colinaService', function ($location, colinaService) {
    var controller = this;
    
    controller.languages = colinaService.getLanguages();
    
    controller.selectLanguage = function (lang) {
        $location.url('/language/' + lang);
    };
}]);

app.controller('MainController', [
    '$routeParams',
    '$location',
    '$window',
    '$timeout',
    'colinaService',
    'translateService',
    function ($routeParams, $location, $window, $timeout, colinaService, translateService) {
        var controller = this;
    
        controller.language = $routeParams.lang;
        if (!colinaService.checkLanguage(controller.language))
            $location.url('/');

        controller.palette = colinaService.getPalette();
        controller.labels = translateService.dictionary;
        controller.commandHistory = [];
        controller.appState = {
            speechApi: 'undefined',
            backend: 'iddle'
        };
        controller.resultUrl = '';

        var recognizer = null;
        $window.SpeechRecognition = $window.SpeechRecognition ||
            $window.webkitSpeechRecognition ||
            null;

        if ($window.SpeechRecognition) {
            recognizer = new $window.SpeechRecognition();
            recognizer.lang = controller.language;
            recognizer.continuous = true;
            recognizer.maxAlternatives = 1;

            recognizer.onstart = function () {
                controller.appState.speechApi = 'listening';
            };

            recognizer.onresult = function (event) {
                recognizer.stop();
                controller.appState.speechApi = 'stopped';

                if (typeof(event.results) === 'undefined') {
                    return;
                }

                if (event.results[0].isFinal) {
                    var command = event.results[0][0].transcript;
                    controller.commandHistory.push(command);

                    controller.appState.backend = 'rendering';
                    
                    colinaService.sendCommand(command, controller.language).then(function (data) {
                        controller.resultUrl = data;
                    }).finally(function () {
                        controller.appState.backend = 'iddle';
                        recognizer.start();
                    });
                }
            };

            recognizer.onerror = function (event) {
                if (event.error == 'no-speech') {
                    try {
                        recognizer.start();
                    }
                    catch (err) {};
                }
                else {
                    console.log(event.error);
                }
            };

            recognizer.start();
        } else {
            controller.appState.speechApi = 'Unsuported API';
        }

        controller.startListening = function () {
            if (recognizer && controller.appState.speechApi == 'stopped') {
                try {
                    recognizer.start();
                } catch (err) { }
            }
        };
        
        controller.stopListening = function () {
            if (recognizer && controller.appState.speechApi == 'listening') {
                recognizer.stop();
                controller.appState.speechApi = 'stopped';
            } 
        };
    }
]);