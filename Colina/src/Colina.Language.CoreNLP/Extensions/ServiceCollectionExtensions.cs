using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Colina.Language.CoreNLP.Analysers;
using Colina.Language.CoreNLP.Recognizers;
using Colina.Language.CoreNLP.Settings;
using Microsoft.Extensions.Configuration;
using Colina.Infrastructure.Extensions;

namespace Colina.Language.CoreNLP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCoreNlp(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.ConfigureAppSettings<StanfordSettings>(configuration.GetSection("Stanford"));

            services.AddScoped<PartOfSpeechAnalyser>();
            services.AddScoped<SentenceRecognizer>();
            services.AddScoped<LanguageSettings>();            
        }
    }
}
