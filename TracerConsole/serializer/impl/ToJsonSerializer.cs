using System.Text;
using Newtonsoft.Json;
using TracerApplication.model;

namespace TracerConsole.serializer.impl;

public class ToJsonSerializer : ISerializer
{
    public StringWriter Serialize(TraceResult traceResult)
    {
        string json = JsonConvert.SerializeObject(traceResult, Formatting.Indented);
        return new StringWriter(new StringBuilder(json));
    }
}