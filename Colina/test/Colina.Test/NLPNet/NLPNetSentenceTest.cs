using Colina.Abstraction.Bootstrap.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Colina.Test.NLPNet
{
    public class NLPNetSentenceTest
    {
        private readonly IServiceProvider _provider;
        private readonly IConfigurationRoot _configuration;

        public NLPNetSentenceTest()
        {
            _configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("nlpnet:ModelsPath", "C:\\Users\\cmendes\\Source\\Repos\\colina\\Colina\\resources\\data\\"),
                                    new KeyValuePair<string, string>("nlpnet:PythonPath", "C:\\Python27\\python.exe"),
                                    new KeyValuePair<string, string>("nlpnet:PythonAppPath", "C:\\Users\\cmendes\\Source\\Repos\\colina\\Colina\\resources\\nlpnet-tag.py")
                                }).Build();

            var services = new ServiceCollection();
            services.AddColinaModules(_configuration);
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void RecognizeSentenceTest()
        {
            //arrange
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            var recognizer = _provider.GetService<ISentenceRecognizer>();

            //act
            var userAction = recognizer.Recognize("Mover cadeira 10 pixels para direita");

            //assert
            Assert.Equal(userAction.Command.Identifier, "Mover");
            Assert.Equal(userAction.Object.Identifier, Guid.Parse("6b0747d1-9efc-43d2-9c7f-8db33cd01ab7"));
        }
    }
}
