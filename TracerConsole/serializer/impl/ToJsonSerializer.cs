using Newtonsoft.Json;
using TracerApplication.model;
using TracerApplication.serializer;

namespace TracerConsole.serializer.impl;

public class ToJsonSerializer : ISerializer
{
    private const string JSON_SAVE_PATH = "D:/123.json";
    public void Serialize(TraceResult traceResult)
    {
        string json = JsonConvert.SerializeObject(traceResult, Formatting.Indented);

        using (StreamWriter writer = new StreamWriter(JSON_SAVE_PATH, false))
        {
            writer.WriteLine(json);
        }
        
        Console.WriteLine(json);
    }
}