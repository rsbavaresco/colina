using Colina.Application.Services;
using Colina.Infrastructure.Extensions;
using Colina.Language.Abstraction.Interfaces;
using Colina.Language.CoreNLP.Analysers;
using Colina.Language.Recognizers;
using Colina.Language.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<BuilderService>();
            services.AddScoped<ISentenceRecognizer, SentenceRecognizer>();
            services.AddScoped<ILanguageSettings, LanguageSettings>();
            services.AddScoped<IPartOfSpeechAnalyser, PartOfSpeechAnalyser>();
            
            services.ConfigureAppSettings<StanfordSettings>(Configuration.GetSection("Stanford"));
            services.ConfigureAppSettings<Colina.Language.NLPNet.Settings.NLPNetSettings>(Configuration.GetSection("nlpnet"));
        }

        public static void AddDesign(this IServiceCollection services)
        {

        }
    }
}
