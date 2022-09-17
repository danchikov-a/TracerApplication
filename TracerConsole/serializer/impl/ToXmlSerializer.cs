using System.Xml;
using System.Xml.Serialization;
using TracerApplication.model;
using TracerConsole.serializer;

namespace TracerApplication.serializer.impl;

public class ToXmlSerializer : ISerializer
{
    public StringWriter Serialize(TraceResult traceResult)
    {
        using (StringWriter stringWriter = new StringWriter())
        {
            using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
            {
                xmlTextWriter.Formatting = Formatting.Indented;
        
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
                xmlSerializer.Serialize(xmlTextWriter, traceResult);
                /*Console.WriteLine(stringWriter.ToString());
                
                using (StreamWriter writer = new StreamWriter(XML_SAVE_PATH, false))
                {
                    writer.WriteLine(stringWriter);
                }*/
            }
            
            return stringWriter;
        }
    }
}