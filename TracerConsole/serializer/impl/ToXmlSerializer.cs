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
            }
            
            return stringWriter;
        }
    }
}