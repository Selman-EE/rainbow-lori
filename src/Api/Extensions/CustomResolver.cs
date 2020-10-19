using Application.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Extensions
{
    internal class CustomResolver : DefaultContractResolver
    {
        private readonly List<string> _namesOfVirtualPropsToKeep = new List<string>();

        public CustomResolver() { }

        public CustomResolver(IEnumerable<string> namesOfVirtualPropsToKeep)
        {
            this._namesOfVirtualPropsToKeep = namesOfVirtualPropsToKeep.Select(x => x.ToLower()).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                //var returnTypeNameSpace = typeof(Result).FullName;
                //var propNamespace = propInfo.DeclaringType.FullName;                

                //for virtual class
                if (!propInfo.GetMethod.IsVirtual || propInfo.GetMethod.IsFinal || _namesOfVirtualPropsToKeep.Contains(propInfo.Name.ToLower()))
                {
                    return prop;
                }
                prop.ShouldSerialize = obj => false;
                return prop;
            }
            return prop;
        }
    }
}
