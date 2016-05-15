using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace CustomDataAccessor
{
    public class CustomDataOptionAccessor : ICustomDataOptionAccessor
    {
        public CustomData GetCustomData(string filePath)
        {
            string jsonDocument;

            using (StreamReader reader = new StreamReader(filePath))
            {
                jsonDocument = reader.ReadToEnd();
            }

            JavaScriptSerializer serailizer = new JavaScriptSerializer();
            CustomData data = serailizer.Deserialize<CustomData>(jsonDocument);

            return data;
        }

        public void SetCustomData(string filePath, CustomData data)
        {
            StringBuilder builder = new StringBuilder();

            JavaScriptSerializer serailizer = new JavaScriptSerializer();
            serailizer.Serialize(data, builder);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }
        }
    }
}
