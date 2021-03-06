﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using System.Globalization;
using Colina.Language.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Colina.Abstraction.Bootstrap.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseLocalization(this IApplicationBuilder app)
        {
            var localization = new RequestLocalizationOptions()
            {
                SupportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US")
                },
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR")                
            };
            app.UseRequestLocalization(localization);
        }

        public static void RetrieveData(this IApplicationBuilder app)
        {
            var repository = app.ApplicationServices.GetService<IDomainRepository>();
            repository.CreateDataSetCache();
        }
    }
}
