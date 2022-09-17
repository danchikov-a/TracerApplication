using System.Diagnostics;
using System.Text.Json.Serialization;

namespace TracerApplication.service.impl;

public class TraceMethod
{
    [JsonPropertyName("name")]
    public string MethodName
    {
        get; private set;
    }

    [JsonPropertyName("class")]
    public string ClassName
    {
        get; private set;
    }

    [JsonPropertyName("time")]
    public double ExecutionTime
    {
        get; private set;
    }

    [JsonPropertyName("methods")]
    public List<TraceMethod> TraceMethods
    {
        get;
        private set;
    }

    private readonly Stopwatch _stopwatch;

    public TraceMethod(string className, string methodName)
    {
        ClassName = className;
        MethodName = methodName;
        TraceMethods = new List<TraceMethod>();
        _stopwatch = new Stopwatch();
    }

    public TraceMethod(string methodName, string className, double executionTime)
    {
        MethodName = methodName;
        ClassName = className;
        ExecutionTime = executionTime;
        TraceMethods = new List<TraceMethod>();
        _stopwatch = new Stopwatch();
    }

    internal TraceMethod GetTraceResult()
    {
        var traceMethod = new TraceMethod(ClassName, MethodName, ExecutionTime);

        foreach (var method in TraceMethods)
        {
            method.TraceMethods.Add(method.GetTraceResult());
        }

        return traceMethod;
    }

    public void StartTrace()
    {
        _stopwatch.Start();
    }

    public void StopTrace()
    {
        _stopwatch.Stop();
        ExecutionTime = _stopwatch.Elapsed.TotalMilliseconds;
    }
}