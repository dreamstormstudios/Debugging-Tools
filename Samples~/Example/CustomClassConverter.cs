using System;
using DebuggingTools.Runtime.Data;
using DebuggingTools.Runtime.Interfaces;

namespace DefaultNamespace
{
    public class CustomClassConverter : IObjectConverter
    {
        public Type HandledType => typeof(CustomClass);
        
        public ObjectLog ConvertToObjectLog(object convertedObject)
        {
            CustomClass customClass = convertedObject as CustomClass;
            
            return new ObjectLog(customClass.Name, $"{customClass.ImportantValue}");
        }
    }
}