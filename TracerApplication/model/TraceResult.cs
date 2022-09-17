using System.Text.Json.Serialization;
using TracerApplication.service.impl;

namespace TracerApplication.model;

public class TraceResult
{
    [JsonPropertyName("threads")]
    public List<TraceThread> Threads
    {
        get;
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