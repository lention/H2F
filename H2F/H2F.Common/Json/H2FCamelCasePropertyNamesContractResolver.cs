using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using H2F.Standard.Common.Reflection;
using H2F.Standard.Common.Timing;
//
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace H2F.Standard .Common.Json
{
    public  class H2FCamelCasePropertyNamesContractResolver: CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            ModifyProperty(member, property);
            return property;
        }

        protected virtual void ModifyProperty(MemberInfo member, JsonProperty property)
        {
            if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
            {
                return;
            }

            if (ReflectionHelper.GetSingleAttributeOfMemberOrDeclaringTypeOrDefault< DisableDateTimeNormalizationAttribute>(member)==null)
            {
                property.Converter = new H2FDateTimeConverter();
            }
        }
    }
}
