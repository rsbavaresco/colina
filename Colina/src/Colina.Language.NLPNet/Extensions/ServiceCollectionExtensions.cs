using Colina.Infrastructure.Extensions;
using Colina.Language.NLPNet.Analysers;
using Colina.Language.NLPNet.Recognizers;
using Colina.Language.NLPNet.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colina.Language.NLPNet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNlpnet(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.ConfigureAppSettings<NLPNetSettings>(configuration.GetSection("nlpnet"));

            services.AddScoped<PartOfSpeechAnalyser>();
            services.AddScoped<SentenceRecognizer>();                                
        }
    }
}
