using System.Diagnostics;

namespace TracerApplication.model;

public class TraceMethod
{
    public string MethodName
    {
        get; 
        set;
    }
    
    public string ClassName
    {
        get;
        set;
    }
    
    public double ExecutionTime
    {
        get;
        set;
    }
    
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

    /*public override string ToString()
    {
        Console.WriteLine(String.Format("{0} {1} {2}", MethodName, ClassName, ExecutionTime));
        Console.WriteLine(TraceMethods.Count == 0);
        Console.WriteLine(String.Join(", ", TraceMethods));
        return base.ToString();
    }*/
}