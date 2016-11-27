using Colina.Abstraction.Bootstrap.Extensions;
using Colina.Data.Repositories.DataTransfersObjects;
using Colina.Data.Repositories.NoSql;
using Colina.Data.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Colina.Test.Data
{
    public class NoSqlRepositoryTest
    {
        private readonly IServiceProvider _provider;
        private readonly IConfigurationRoot _configuration;

        public NoSqlRepositoryTest()
        {
            _configuration = new ConfigurationBuilder()
                                .AddInMemoryCollection(new List<KeyValuePair<string, string>>()
                                {
                                    new KeyValuePair<string, string>("NoSql:ConnectionString", "mongodb://coladm:salsicha@ds048719.mlab.com:48719/colina")
                                }).Build();

            var services = new ServiceCollection();
            services.AddColinaModules(_configuration);
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void GetEnvironments()
        {
            //arrange
            var domainRepository = new EnvironmentNoSqlRepository(_provider.GetRequiredService<NoSqlSettings>());
            var sessionId = Guid.Parse("07a8d50f-3f87-4335-b9ad-6801d9af08e2");

            //act
            var environment = domainRepository.GetByUserSession(sessionId);
            
            //assert
            Assert.Equal(environment.SessionId, sessionId);
        }

        [Fact]
        public void InsertEnvironment()
        {
            //arrange
            var domainRepository = new EnvironmentNoSqlRepository(_provider.GetRequiredService<NoSqlSettings>());
            var sessionId = Guid.NewGuid();
            var imageUniqueId = Guid.NewGuid();
            var newEnvironment = EnvironmentDto.Create(sessionId);
            newEnvironment.AddItem(EnvironmentItemDto.Create(imageUniqueId, 30, 25));

            //act
            domainRepository.Insert(newEnvironment);

            //assert
            var environment = domainRepository.GetByUserSession(sessionId);
            Assert.Equal(environment.SessionId, sessionId);
            Assert.Equal(environment.Items.Count, 1);
            Assert.Equal(environment.Items[0].ImageUniqueId, imageUniqueId);
        }

        [Fact]
        public void UpdateEnvironment()
        {
            //arrange
            var random = new Random();
            var domainRepository = new EnvironmentNoSqlRepository(_provider.GetRequiredService<NoSqlSettings>());
            var sessionId = Guid.Parse("07a8d50f-3f87-4335-b9ad-6801d9af08e2");
            var existingEnvironment = domainRepository.GetByUserSession(sessionId);
            var newItem = EnvironmentItemDto.Create(imageUniqueId: Guid.NewGuid(), x: random.Next(0, 300), y: random.Next(0, 300));

            //act
            existingEnvironment.AddItem(newItem);
            domainRepository.Update(existingEnvironment);

            //assert
            var environment = domainRepository.GetByUserSession(sessionId);
            Assert.Equal(environment.SessionId, sessionId);
            Assert.Equal(environment.Items.Count, existingEnvironment.Items.Count);
            for (int i = 0; i < existingEnvironment.Items.Count; i++)
            {
                Assert.Equal(existingEnvironment.Items[i].ImageUniqueId, environment.Items[i].ImageUniqueId);
            }
        }
    }
}
