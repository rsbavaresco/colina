var app = angular.module('colinaApp');

app.value('imageSource', 'images/');
app.value('apiUrl', 'http://localhost:59483/api/Builder');

app.service('colinaService', ['imageSource', 'apiUrl', '$http', function (imageSource, apiUrl, $http) {
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
                '9df8733f-7fbe-4426-8c7f-7324faf00a7b': imageSource + '9df8733f-7fbe-4426-8c7f-7324faf00a7b.png',
                'ed571669-c4d0-4e58-b65e-9ca37b5281c8': imageSource + 'ed571669-c4d0-4e58-b65e-9ca37b5281c8.png',
                '0c535551-c29c-47d4-bba9-011d67eee14f': imageSource + '0c535551-c29c-47d4-bba9-011d67eee14f.png',
                '4fb6a6a3-b89c-4ad2-bf12-5ef00875ab24': imageSource + '4fb6a6a3-b89c-4ad2-bf12-5ef00875ab24.png',
                '5f9d10ee-2bbb-4f32-a304-363c09daac59': imageSource + '5f9d10ee-2bbb-4f32-a304-363c09daac59.png',
                '9a8526b4-63f5-482b-9fcd-b3f62dadbc3d': imageSource + '9a8526b4-63f5-482b-9fcd-b3f62dadbc3d.png',
                '24ce9363-8fac-413a-bc15-8fe56abc5a74': imageSource + '24ce9363-8fac-413a-bc15-8fe56abc5a74.png',
                '30b5234f-d6c2-47f9-a427-87777b29c56a': imageSource + '30b5234f-d6c2-47f9-a427-87777b29c56a.png',
                '3441ceda-5f8a-44cb-b620-af720135a39e': imageSource + '3441ceda-5f8a-44cb-b620-af720135a39e.png',
                'd50c3cad-0754-4299-9d1a-0091fe0e07fd': imageSource + 'd50c3cad-0754-4299-9d1a-0091fe0e07fd.png',
                '61536148-d877-4fda-9978-32218f211933': imageSource + '61536148-d877-4fda-9978-32218f211933.png',
                '16ed2d9c-52d7-4cea-8e21-8ad0b29e858d': imageSource + '16ed2d9c-52d7-4cea-8e21-8ad0b29e858d.png',
                'cb942e2a-3705-42cc-9427-8813a72c748b': imageSource + 'cb942e2a-3705-42cc-9427-8813a72c748b.png'
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
        '9df8733f-7fbe-4426-8c7f-7324faf00a7b': {
            'pt-BR': 'Mesa',
            'en-US': 'Table'
        },
        '0c535551-c29c-47d4-bba9-011d67eee14f': {
            'pt-BR': 'Cadeira de lado',
            'en-US': 'Sided chair'
        },
        '4fb6a6a3-b89c-4ad2-bf12-5ef00875ab24': {
            'pt-BR': 'Relógio',
            'en-US': 'Clock'
        },
        '5f9d10ee-2bbb-4f32-a304-363c09daac59': {
            'pt-BR': 'Xícara',
            'en-US': 'Cup'
        },
        '9a8526b4-63f5-482b-9fcd-b3f62dadbc3d': {
            'pt-BR': 'Vaso de flor',
            'en-US': 'Flower vase'
        },
        '24ce9363-8fac-413a-bc15-8fe56abc5a74': {
            'pt-BR': 'Lixeira',
            'en-US': 'Trash can'
        },
        '30b5234f-d6c2-47f9-a427-87777b29c56a': {
            'pt-BR': 'Flor',
            'en-US': 'Flower'
        },
        '3441ceda-5f8a-44cb-b620-af720135a39e': {
            'pt-BR': 'Sofá',
            'en-US': 'Couch'
        },
        'd50c3cad-0754-4299-9d1a-0091fe0e07fd': {
            'pt-BR': 'Tapete',
            'en-US': 'Carpet'
        },
        'ed571669-c4d0-4e58-b65e-9ca37b5281c8': {
            'pt-BR': 'Cadeira',
            'en-US': 'Chair'
        },
        '61536148-d877-4fda-9978-32218f211933': {
            'pt-BR': 'Quadro',
            'en-US': 'Picture-frame'
        },
        '16ed2d9c-52d7-4cea-8e21-8ad0b29e858d': {
            'pt-BR': 'Computador',
            'en-US': 'Computer'
        },
        'cb942e2a-3705-42cc-9427-8813a72c748b': {
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
        }
    };
    
    return {
        dictionary: dictionary
    };
}]);