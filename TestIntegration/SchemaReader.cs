using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestIntegration
{
 public class SchemaReader
    {
        public static JSchema GetSchema(string endpoint)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            string jsonDirectory = string.Format(@"{0}\Schemas\{1}.json", projectDirectory, endpoint);
            JSchema schema = JSchema.Parse(File.ReadAllText(jsonDirectory));
            return schema;
        }
    }
}
