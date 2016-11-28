var app = angular.module('colinaApp');

app.value('imageSource', 'images/');
app.value('apiUrl', 'http://localhost:59483/api/Builder');

app.service('colinaService', ['imageSource', 'apiUrl', '$http', '$window', '$q', '$timeout', function (imageSource, apiUrl, $http, $window, $q, $timeout) {
    var sessionId = (function () {
        var fmt = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx';

        return fmt.replace(/[xy]/g, function(c) {
            var r = Math.random()*16|0, v = c === 'x' ? r : (r&0x3|0x8);
            return v.toString(16);
        });
    }());
    
    return {
        getLanguages: function () {
            return [
                {
                    name: 'Português - Brasileiro',
                    code: 'pt-BR',
                    iconPath: imageSource + 'pt-br.png'
                },
                {
                    name: 'English - USA',
                    code: 'en-US',
                    iconPath: imageSource + 'en-us.png'
                }
            ];
        },
        checkLanguage: function (code) {
            if (code == 'pt-BR' || code == 'en-US')
                return true;
            else
                return false;
        },
        getPalette: function (language) {
            return {
                'table': imageSource + 'table.jpg',
                'chair': imageSource + 'chair.jpg',
                'bed': imageSource + 'bed.jpg',
                'shelf': imageSource + 'shelf.jpg',
                'book': imageSource + 'book.jpg',
                'desk': imageSource + 'desk.jpg',
                'chandelier': imageSource + 'chandelier.jpg',
                'picture-frame': imageSource + 'picture-frame.jpg',
                'computer': imageSource + 'computer.jpg',
                'lampshade': imageSource + 'lampshade.jpg',
                'nightstand': imageSource + 'nightstand.jpg'
            };
        },
        sendCommand: function (command, language) {
            return $http.post(
                apiUrl,
                { value: command },
                {
                    headers: {
                        'Colina-Session-ID': sessionId,
                        'Accept-Language': language
                    },
                    responseType: 'arraybuffer'
                }).then(function (data) {
                    var blob = new Blob([data.data], { type: 'image/png' });
                    var objectUrl = URL.createObjectURL(blob);
                    
                    return objectUrl;
                });
        }
    };
}]);

app.service('translateService', [function () {
    var dictionary = {
        'paletteTitle': {
            'pt-BR': 'Paleta de objetos',
            'en-US': 'Object palette'
        },
        'table': {
            'pt-BR': 'Mesa',
            'en-US': 'Table'
        },
        'chair': {
            'pt-BR': 'Cadeira',
            'en-US': 'Chair'
        },
        'bed': {
            'pt-BR': 'Cama',
            'en-US': 'Bed'
        },
        'shelf': {
            'pt-BR': 'Estante',
            'en-US': 'Shelf'
        },
        'book': {
            'pt-BR': 'Livro',
            'en-US': 'Book'
        },
        'desk': {
            'pt-BR': 'Escrivaninha',
            'en-US': 'Desk'
        },
        'chandelier': {
            'pt-BR': 'Lustre',
            'en-US': 'Chandelier'
        },
        'picture-frame': {
            'pt-BR': 'Quadro',
            'en-US': 'Picture-frame'
        },
        'computer': {
            'pt-BR': 'Computador',
            'en-US': 'Computer'
        },
        'lampshade': {
            'pt-BR': 'Abajur',
            'en-US': 'Lampshade'
        },
        'nightstand': {
            'pt-BR': 'Criado Mudo',
            'en-US': 'Nightstand'
        },
        'stop': {
            'pt-BR': 'Parar',
            'en-US': 'Stop'
        },
        'start': {
            'pt-BR': 'Iniciar',
            'en-US': 'Start'
        },
        'listening': {
            'pt-BR': 'Ouvindo',
            'en-US': 'Listening'
        },
        'historyTitle': {
            'pt-BR': 'Histórico de comandos',
            'en-US': 'Command history'
        },
        'example': {
            'pt-BR': '',
            'en-US': ''
        }
    };
    
    return {
        dictionary: dictionary
    };
}]);