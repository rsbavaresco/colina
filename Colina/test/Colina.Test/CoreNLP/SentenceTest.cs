using Colina.Abstraction.Bootstrap.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Colina.Test.CoreNLP
{
    public class SentenceTest
    {
        private readonly IServiceProvider _provider;
        private readonly IConfigurationRoot _configuration;

        public SentenceTest()
        {
            _configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("Stanford:ModelsPath", @"..\..\..\..\..\..\resources\stanford-english-corenlp-2016-01-10-models\")
                                }).Build();

            var services = new ServiceCollection();
            services.AddColinaModules(_configuration);
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void RecognizeSentenceTest()
        {
            var recognizer = _provider.GetService<ISentenceRecognizer>();
            recognizer.Recognize("Move a ball into the hole");
        }
    }
}
