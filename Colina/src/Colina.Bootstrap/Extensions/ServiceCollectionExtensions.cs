using Colina.Abstraction.Bootstrap.Providers;
using Colina.Application.Services;
using Colina.Data.Repositories;
using Colina.Data.Settings;
using Colina.Design.Abstraction.Interfaces;
using Colina.Design.Drawings;
using Colina.Design.Settings;
using Colina.Infrastructure.DataAccess;
using Colina.Infrastructure.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.CoreNLP.Extensions;
using Colina.Language.Domain.Repositories;
using Colina.Language.NLPNet.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Colina.Abstraction.Bootstrap.Extensions
{
    public static class ServiceCollectionExtensions
    {        
        public static void AddColinaModules(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.ConfigureAppSettings<DesignSettings>(configuration.GetSection("Design"));
            services.ConfigureAppSettings<OntologySettings>(configuration.GetSection("Ontology"));
            services.ConfigureAppSettings<NoSqlSettings>(configuration.GetSection("NoSql"));


            services.AddLanguage();
            services.AddDesign();
            services.AddCoreNlp(configuration);
            services.AddNlpnet(configuration);
            services.AddMemoryCache();
        }

        private static void AddLanguage(this IServiceCollection services)
        {            
            services.AddScoped<BuilderService>();
            services.AddScoped<ITypeProvider, TypeProvider>();
            services.AddScoped<IDomainRepository, NoSqlDomainRepository>();
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
            services.AddScoped<IImageDrawer, ImageDrawer>();
            services.AddScoped<IImageReader, ImageReader>();
        }
    }
}
