using TracerApplication.service.impl;

namespace TracerApplication.model;

public class TraceResult
{
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