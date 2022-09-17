using Newtonsoft.Json;
using TracerApplication.model;
using TracerApplication.serializer;

namespace TracerConsole.serializer.impl;

public class ToJsonSerializer : ISerializer
{
    public void Serialize(TraceResult traceResult)
    {
        string json = JsonConvert.SerializeObject(traceResult, Formatting.Indented);

        using (StreamWriter writer = new StreamWriter("D:/123.json", false))
        {
            writer.WriteLine(json);
        }
        
        Console.WriteLine(json);
    }
}