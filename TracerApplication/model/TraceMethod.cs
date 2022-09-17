using System.Diagnostics;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace TracerApplication.model;

public class TraceMethod
{
    [JsonProperty("name")]
    [XmlAttribute("name")]
    public string MethodName
    {
        get; 
        set;
    }
    
    [JsonProperty("class")]
    [XmlAttribute("class")]
    public string ClassName
    {
        get;
        set;
    }
    
    [JsonProperty("time")]
    [XmlAttribute("time")]
    public double ExecutionTime
    {
        get;
        set;
    }
    
    [JsonProperty("methods")]
    [XmlElement("method")]
    public List<TraceMethod> TraceMethods
    {
        get;
        set;
    }

    private readonly Stopwatch _stopwatch;

    public TraceMethod()
    {
    }

    public TraceMethod(string className, string methodName)
    {
        ClassName = className;
        MethodName = methodName;
        TraceMethods = new List<TraceMethod>();
        _stopwatch = new Stopwatch();
    }

    public TraceMethod(string className, string methodName, double executionTime)
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
            traceMethod.TraceMethods.Add(method.GetTraceResult());
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