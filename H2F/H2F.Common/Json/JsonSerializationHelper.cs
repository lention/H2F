using System;
using System.Collections.Generic;
using System.Text;
//
using H2F.Standard.Common.Extensions;
using Newtonsoft.Json;
namespace H2F.Standard .Common.Json
{
    public static class JsonSerializationHelper
    {
        private const char TypeSeperator = '|';

        public static string SerializeWithType(object obj)
        {
            return SerializeWithType(obj, obj.GetType());
        }
        public static string SerializeWithType(object obj, Type type)
        {
            var serialized = obj.ToJsonString();

            return "{0}{1}{2}".Format(new object[] { type.AssemblyQualifiedName, TypeSeperator, serialized });
        }

        public static object DeserializeWithType(string serializedObj)
        {
            var typeSeperatorIndex = serializedObj.IndexOf(TypeSeperator);
            var type = Type.GetType(serializedObj.Substring(0, typeSeperatorIndex));
            var serialized = serializedObj.Substring(typeSeperatorIndex + 1);

            var options = new JsonSerializerSettings
            {
                ContractResolver = new H2FCamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.DeserializeObject(serialized, type, options);
        }
    }
}
