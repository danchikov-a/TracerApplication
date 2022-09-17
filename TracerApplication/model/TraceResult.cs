using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TracerApplication.model;

[XmlType("root")]
public class TraceResult
{
    [JsonProperty("threads")]
    [XmlElement("thread")]
    public List<TraceThread> Threads
    {
        get;
    }

    public TraceResult()
    {
    }

    public TraceResult(Dictionary<int, TraceThread> threads)
    {
        Threads = new List<TraceThread>();

        foreach (var traceThread in threads.Select(thread => thread.Value))
        {
            Threads.Add(traceThread.GetTraceResult());
        }
    }
}