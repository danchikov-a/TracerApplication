using System.Text.Json.Serialization;

namespace TracerApplication.service.impl;

public class TraceThread
{
    [JsonPropertyName("id")]
    public int Id
    {
        get; private set;
    }

    [JsonPropertyName("time")]
    public double FinalExecutionTime
    {
        get; private set;
    }

    [JsonPropertyName("methods")]
    public List<TraceMethod> TraceMethods
    {
        get; private set;
    }

    private Stack<TraceMethod> StillWorkingMethods; 

    public TraceThread(int id)
    {
        Id = id;
        TraceMethods = new List<TraceMethod>();
        StillWorkingMethods = new Stack<TraceMethod>();
    }

    public TraceThread(int id, double finalExecutionTime)
    {
        Id = id;
        FinalExecutionTime = finalExecutionTime;
        TraceMethods = new List<TraceMethod>();
        StillWorkingMethods = new Stack<TraceMethod>();
    }

    internal TraceThread GetTraceResult()
    {
        var traceThread = new TraceThread(Id, FinalExecutionTime);

        foreach (var method in TraceMethods)
        {
            traceThread.TraceMethods.Add(method.GetTraceResult());
        }

        return traceThread;
    }

    public void StartTrace(TraceMethod method)
    {
        if (StillWorkingMethods.Count > 0)
        {
            var lastWorkingMethod = StillWorkingMethods.Peek();
            lastWorkingMethod.TraceMethods.Add(method);
        }

        method.StartTrace();
        StillWorkingMethods.Push(method);
    }

    public void StopTrace()
    {
        var lastWorkingMethod = StillWorkingMethods.Pop();
        lastWorkingMethod.StopTrace();

        if (StillWorkingMethods.Count == 0)
        {
            TraceMethods.Add(lastWorkingMethod);
            FinalExecutionTime += lastWorkingMethod.ExecutionTime;
        }
    }
}