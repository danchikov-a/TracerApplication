using System.Diagnostics;
using TracerApplication.model;

namespace TracerApplication.service.impl;

public class Tracer : ITracer
{
    private const int FRAME_INDEX = 1;
    private static readonly object LockObject = new();
    
    private readonly Dictionary<int, TraceThread> _traceThreads;

    public Tracer()
    {
        _traceThreads = new Dictionary<int, TraceThread>();
    }

    public TraceResult GetTraceResult()
    {
        lock (LockObject)
        {
            return new TraceResult(_traceThreads);
        }
    }

    public void StartTrace()
    {
        var method = new StackTrace().GetFrame(FRAME_INDEX).GetMethod();
        var methodTracer = new TraceMethod(method.ReflectedType.Name, method.Name);
        var threadTracer = GetThreadTracer(Thread.CurrentThread.ManagedThreadId);
        threadTracer.StartTrace(methodTracer);
    }

    public void StopTrace()
    {
        GetThreadTracer(Thread.CurrentThread.ManagedThreadId).StopTrace();
    }

    private TraceThread GetThreadTracer(int id)
    {
        lock (LockObject)
        {
            if (!_traceThreads.TryGetValue(id, out var thread))
            {
                thread = new TraceThread(id);
                _traceThreads.Add(id, thread);
            }

            return thread;
        }
    }
}