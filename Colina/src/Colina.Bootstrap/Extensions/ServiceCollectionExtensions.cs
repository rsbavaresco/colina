using Colina.Infrastructure.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.Recognizers;
using Colina.Language.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Abstraction.Bootstrap.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static IConfigurationRoot Configuration;
        public static void AddColinaModules(this IServiceCollection services, IConfigurationRoot configuration)
        {
            Configuration = configuration;

            services.AddLanguage();
            services.AddDesign();
        }

        public static void AddLanguage(this IServiceCollection services)
        {
            services.AddScoped<ISentenceRecognizer, SentenceRecognizer>();
            services.AddScoped<ILanguageSettings, LanguageSettings>();

            services.ConfigureAppSettings<StanfordSettings>(Configuration.GetSection("Stanford"));
        }

        public static void AddDesign(this IServiceCollection services)
        {

        }
    }
}
