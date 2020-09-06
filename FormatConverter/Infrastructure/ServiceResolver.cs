using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FormatConverter.Infrastructure
{
    public static class ServiceResolver
    {
        public static IEnumerable<T> GetImplementationsOf<T>(params object[] constructorArgs) where T : class
        {
            var domainTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes());

            var types = typeof(T).IsInterface 
                ? domainTypes.Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract) 
                : domainTypes.Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T)));

            return types.Select(type => (T) Activator.CreateInstance(type, constructorArgs)).ToList();
        }
    }
}