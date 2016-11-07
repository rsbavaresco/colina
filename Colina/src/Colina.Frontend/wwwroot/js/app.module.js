var app = angular.module('colinaApp', [
    'ngMaterial'
]);



(function () {
    // Test browser support
    window.SpeechRecognition = window.SpeechRecognition ||
                               window.webkitSpeechRecognition ||
                               null;

    // Caso não suporte esta API DE VOZ
    if (window.SpeechRecognition === null) {
        $('#ws-unsupported').removeClass('hidden');
        $('#gravar i').attr('style', 'box-shadow: inset 0 0 20px 100px red;color:#000;');
    } else {
        var recognizer = new window.SpeechRecognition();
        var transcription = $('transcription');

        recognizer.lang = 'pt-BR';

        //Para o reconhecedor de voz, não parar de ouvir, mesmo que tenha pausas no usuario
        recognizer.continuous = true;
        recognizer.onresult = function (event) {
            transcription.textContent = "";
            for (var i = event.resultIndex; i < event.results.length; i++) {
                if (event.results[i].isFinal) {
                    transcription.textContent = event.results[i][0].transcript + ' (Taxa de acerto [0/1] : ' + event.results[i][0].confidence + ')';
                } else {
                    transcription.textContent += event.results[i][0].transcript;
                }
            }
        }

        var started = false;

        $('#gravar i').click(function () {
            try {
                if (started) {
                    recognizer.stop();
                    $('#status span').removeClass('gravando').html('parado');
                } else {
                    recognizer.start();
                    $('#status span').addClass('gravando').html('gravando');
                }
                started = !started;
            } catch (ex) {
                alert("error: " + ex.message);
            }
        });
    }
})