var app = angular.module('colinaApp');

app.value('imageSource', 'http://localhost:5000/images/');

app.service('colinaService', ['imageSource', '$http', function (imageSource, $http) {
    var currentLanguage = 'pt-BR';

    var objectNames = {
        'en-US': {
            'table': 'Table',
            'chair': 'Chair',
            'bed': 'Bed',
            'shelf': 'Shelf',
            'book': 'Book',
            'desk': 'Desk',
            'chandelier': 'Chandelier',
            'picture-frame': 'Picture-frame',
            'computer': 'Computer',
            'lampshade': 'Lampshade',
            'nightstand': 'Nightstand'
        },
        'pt-BR': {
            'table': 'Mesa',
            'chair': 'Cadeira',
            'bed': 'Cama',
            'shelf': 'Estante',
            'book': 'Livro',
            'desk': 'Escrivaninha',
            'chandelier': 'Lustre',
            'picture-frame': 'Quadro',
            'computer': 'Computador',
            'lampshade': 'Abajur',
            'nightstand': 'Criado Mudo'
        }
    };

    return {
        getPalette: function () {
            return [
                {
                    name: objectNames[currentLanguage]['table'],
                    imageUrl: imageSource + 'table.jpg'
                },
                {
                    name: objectNames[currentLanguage]['chair'],
                    imageUrl: imageSource + 'chair.jpg'
                },
                {
                    name: objectNames[currentLanguage]['bed'],
                    imageUrl: imageSource + 'bed.jpg'
                },
                {
                    name: objectNames[currentLanguage]['shelf'],
                    imageUrl: imageSource + 'shelf.jpg'
                },
                {
                    name: objectNames[currentLanguage]['book'],
                    imageUrl: imageSource + 'book.jpg'
                },
                {
                    name: objectNames[currentLanguage]['desk'],
                    imageUrl: imageSource + 'desk.jpg'
                },
                {
                    name: objectNames[currentLanguage]['chandelier'],
                    imageUrl: imageSource + 'chandelier.jpg'
                },
                {
                    name: objectNames[currentLanguage]['picture-frame'],
                    imageUrl: imageSource + 'picture-frame.jpg'
                },
                {
                    name: objectNames[currentLanguage]['computer'],
                    imageUrl: imageSource + 'computer.jpg'
                },
                {
                    name: objectNames[currentLanguage]['lampshade'],
                    imageUrl: imageSource + 'lampshade.jpg'
                },
                {
                    name: objectNames[currentLanguage]['nightstand'],
                    imageUrl: imageSource + 'nightstand.jpg'
                }
            ]
        },
        getLanguage: function () {
            return currentLanguage;
        },
        setLanguage: function (language) {
            currentLanguage = language;
        },
        sendCommand: function (command) {
            $http.post('http://localhost:59483/api/Builder', { value: command });
        }
    };
}]);