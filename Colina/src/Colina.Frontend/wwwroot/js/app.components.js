var app = angular.module('colinaApp');

app.directive('wsColina', ['$window', 'colinaService', function ($window, colinaService) {
    var started = false;

    return {
        link: function (scope, elem, attrs) {
            $window.SpeechRecognition = $window.SpeechRecognition ||
                $window.webkitSpeechRecognition ||
                null;

            if ($window.SpeechRecognition) {
                var recognizer = new $window.SpeechRecognition();

                recognizer.lang = colinaService.getLanguage();
                recognizer.continuous = true;

                recognizer.onresult = function (event) {
                    if (event.results[0].isFinal) {
                        colinaService.sendCommand(event.results[0][0].transcript)
                    }
                };

                elem.on('click', function () {
                    if (started) {
                        elem.find('i').removeClass('fa-microphone').addClass('fa-microphone-slash');
                        recognizer.stop();
                    } else {
                        elem.find('i').removeClass('fa-microphone-slash').addClass('fa-microphone');
                        recognizer.start();
                    }

                    started = !started;
                });
            } else {

            }
        }
    };
}]);