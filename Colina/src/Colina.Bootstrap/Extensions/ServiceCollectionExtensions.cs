using Colina.Abstraction.Bootstrap.Providers;
using Colina.Application.Services;
using Colina.Infrastructure.DataAccess;
using Colina.Infrastructure.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.CoreNLP.Extensions;
using Colina.Language.Domain.Repositories;
using Colina.Language.NLPNet.Extensions;
using Colina.Language.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Colina.Abstraction.Bootstrap.Extensions
{
    public static class ServiceCollectionExtensions
    {        
        public static void AddColinaModules(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.ConfigureAppSettings<OntologySettings>(configuration.GetSection("Ontology"));


            services.AddLanguage();
            services.AddDesign();
            services.AddCoreNlp(configuration);
            services.AddNlpnet(configuration);            
        }

        private static void AddLanguage(this IServiceCollection services)
        {            
            services.AddScoped<BuilderService>();
            services.AddScoped<ITypeProvider, TypeProvider>();
            services.AddScoped<IDomainRepository, DomainRepository>();
            services.AddScoped(TypeProviderFactory<ISentenceRecognizer>);
            services.AddScoped(TypeProviderFactory<ILanguageSettings>);            
            services.AddScoped(TypeProviderFactory<IPartOfSpeechAnalyser>);
        }

        private static T TypeProviderFactory<T>(IServiceProvider serviceProvider) where T : class
        {
            return serviceProvider.GetRequiredService<ITypeProvider>()
                                  .Provides<T>();            
        }

        private static void AddDesign(this IServiceCollection services)
        {

        }
    }
}
