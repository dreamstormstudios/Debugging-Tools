using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DebuggingTools.Runtime.Interfaces;

namespace DebuggingTools.Runtime
{
    public static class ObjectConvertersFactory
    {
        #region Private Fields

        private static bool isInitialized;
        private static Dictionary<Type, IObjectConverter> objectConverters;

        #endregion

        #region Public Methods

        public static IObjectConverter ConverterByHandledType(Type handledType)
        {
            if (!isInitialized)
            {
                Initialize();
            }

            objectConverters.TryGetValue(handledType, out IObjectConverter objectConverter);

            return objectConverter;
        }

        #endregion

        #region Private Methods

        private static void Initialize()
        {
            IEnumerable<Type> validTypes = GetAllTypes().Where(ImplementsInterface);
            objectConverters = new Dictionary<Type, IObjectConverter>();

            foreach (Type type in validTypes)
            {
                IObjectConverter objectConverter = Activator.CreateInstance(type) as IObjectConverter;
                objectConverters.Add(objectConverter.HandledType, objectConverter);
            }

            isInitialized = true;
        }

        private static IEnumerable<Type> GetAllTypes()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            return assemblies.SelectMany(assembly => assembly.GetTypes());
        }

        private static bool ImplementsInterface(Type type)
        {
            return type.GetInterfaces().Contains(typeof(IObjectConverter)) && !type.IsInterface && !type.IsAbstract;
        }

        #endregion
    }
}