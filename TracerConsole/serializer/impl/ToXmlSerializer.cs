using System.Xml;
using System.Xml.Serialization;
using TracerApplication.model;
using TracerConsole.serializer;

namespace TracerApplication.serializer.impl;

public class ToXmlSerializer : ISerializer
{
    public void Serialize(TraceResult traceResult)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter tw = new XmlTextWriter(sw);
        tw.Formatting = Formatting.Indented;
        
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(TraceResult));
        xmlSerializer.Serialize(tw, traceResult);
        
        tw.Close();
        sw.Close();
        
        Console.WriteLine(sw.ToString());
    }
}