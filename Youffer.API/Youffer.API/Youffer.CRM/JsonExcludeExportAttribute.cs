// ---------------------------------------------------------------------------------------------------
// <copyright file="JsonExcludeExportAttribute.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-18</date>
// <summary>
//     The JsonExcludeExportAttribute class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.CRM
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;

    using Jayrock.Json;
    using Jayrock.Json.Conversion;

    [Serializable]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class JsonExcludeExportAttribute : Attribute, IPropertyDescriptorCustomization, IObjectMemberExporter
    {
        public void Apply(PropertyDescriptor property)
        {
            var services = (IServiceContainer)property;
            services.AddService(typeof(IObjectMemberExporter), this);
        }

        void IObjectMemberExporter.Export(ExportContext context, JsonWriter writer, object source)
        {
            //writer.WriteMember(_property.Name);
            //context.Export(_property.GetValue(source), writer);
        }
    }
}
