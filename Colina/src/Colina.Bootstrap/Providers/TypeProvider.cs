using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Colina.Abstraction.Bootstrap.Providers
{
    internal class TypeProvider : ITypeProvider
    {        
        private readonly IServiceProvider _serviceProvider;
        private readonly string _cultureName;

        public TypeProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _cultureName = CultureInfo.CurrentCulture.Name;
        }

        public T Provides<T>() where T : class
        {
            switch (_cultureName)
            {
                case "pt-BR":
                     {
                        return FindInAssembly<T>("Colina.Language.NLPNet");
                     }
                case "en-US":
                    {
                        return FindInAssembly<T>("Colina.Language.CoreNLP");
                    }
                default: return null;
            }
        }
        
        private T FindInAssembly<T>(string assembly) where T : class
        {
            var module = Assembly.Load(new AssemblyName(assembly));

            var type = module.ExportedTypes
                             .Where(t => typeof(T).IsAssignableFrom(t))
                             .FirstOrDefault();

            return _serviceProvider.GetService(type) as T;
        }
    }
}
