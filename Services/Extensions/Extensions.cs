using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotLiquid;
using Emailer.Entities;
using Microsoft.AspNetCore.Builder;

namespace Emailer.Services.Extensions
{
    public static class Extensions
    {
        public static IApplicationBuilder RegisterSafeTypeForEmails(this IApplicationBuilder app)
        {
            var coreNamespace = "Emailer.Entities";
            var coreAssembly = typeof(EmailMessage).GetTypeInfo().Assembly;

            var typesToRegister = GetTypesInNamespace(coreAssembly, coreNamespace);
            foreach (var type in typesToRegister)
            {
                if (type.IsEnum)
                {
                    Template.RegisterSafeType(type, o => o.ToString());
                }
                else
                {
                    Template.RegisterSafeType(type, new[] { type.Name });
                }
            }

            return app;
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            var result = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                if (type.Namespace != null)
                {
                    if (type.Namespace.StartsWith(nameSpace, StringComparison.Ordinal))
                    {
                        result.Add(type);
                    }
                }
            }

            return result.ToArray();
        }
    }
}