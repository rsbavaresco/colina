using Colina.Abstraction.Bootstrap.Extensions;
using Colina.Application.Services;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                                    new KeyValuePair<string, string>("Stanford:ModelsPath", @"..\..\..\..\..\..\resources\stanford-english-corenlp-2016-01-10-models\"),
                                    new KeyValuePair<string, string>("Ontology:OwlPath", @"..\..\..\..\..\..\resources\\colinaOntology.owl"),
                                    new KeyValuePair<string, string>("NoSql:ConnectionString", "mongodb://localhost:27017/colina"),
                                    new KeyValuePair<string, string>("Design:Unix:PathToImages", "/mnt/f/tools/torch/distro/myscripts/images"),
                                    new KeyValuePair<string, string>("Design:Unix:PathToObjects", "/mnt/f/tools/torch/distro/myscripts/objects"),
                                    new KeyValuePair<string, string>("Design:Unix:PathToData", "/mnt/f/tools/torch/distro/myscripts/data"),
                                    new KeyValuePair<string, string>("Design:Windows:PathToImages", "F:\\tools\\torch\\distro\\myscripts\\images")
                                }).Build();

            var services = new ServiceCollection();
            services.AddColinaModules(_configuration);            
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void RecognizeSentenceTest()
        {
            //arrange
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            var repository = _provider.GetRequiredService<IDomainRepository>();
            repository.CreateDataSetCache();

            var recognizer = _provider.GetService<ISentenceRecognizer>();

            //act
            var userAction = recognizer.Recognize("Move a chair one centimeter in front");

            //assert
            Assert.Equal(userAction.Command.Identifier, "Move");
            Assert.Equal(userAction.Object.Identifier, Guid.Parse("6b0747d1-9efc-43d2-9c7f-8db33cd01ab7"));
        }

        [Fact]
        public void BuilderTest()
        {
            //arrange
            var sessionId = Guid.Parse("82c11c88-207c-524e-91f7-454840232cfa");
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            var repository = _provider.GetRequiredService<IDomainRepository>();
            repository.CreateDataSetCache();

            var service = _provider.GetService<BuilderService>();

            //act
            var builderContent = service.Build(sessionId, "Move a chair one centimeter in front");
        }
    }
}
