using NUnit.Framework;
using TracerApplication.service.impl;

namespace TracerTest;

public class Test
{
    private const int SleepTime = 40;
    private const int ThreadsCount = 3;

    private Tracer _tracer;

    [SetUp]
    public void SetupBeforeEachTest()
    {
        _tracer = new Tracer();
    }

    private void SingleMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(SleepTime);
        _tracer.StopTrace();
    }

    private void MethodWithInnerMethod()
    {
        _tracer.StartTrace();
        Thread.Sleep(SleepTime);
        SingleMethod();
        _tracer.StopTrace();
    }

    [Test]
    public void TestSingleMethod()
    {
        SingleMethod();
        var traceResult = _tracer.GetTraceResult();
        var firstTraceMethod = traceResult.Threads[0].TraceMethods[0];
        
        Assert.AreEqual(nameof(SingleMethod), firstTraceMethod.MethodName);
        Assert.AreEqual(nameof(Test), firstTraceMethod.ClassName);
        Assert.AreEqual(0, firstTraceMethod.TraceMethods.Count);
    }

    [Test]
    public void TestMethodWithInnerMethod()
    {
        MethodWithInnerMethod();
        var traceResult = _tracer.GetTraceResult();
        var firstTraceMethod = traceResult.Threads[0].TraceMethods[0];
        
        Assert.AreEqual(nameof(MethodWithInnerMethod), firstTraceMethod.MethodName);
        Assert.AreEqual(nameof(SingleMethod), firstTraceMethod.TraceMethods[0].MethodName);
        Assert.AreEqual(nameof(Test), firstTraceMethod.ClassName);
        Assert.AreEqual(1, firstTraceMethod.TraceMethods.Count);
        Assert.AreEqual(0, firstTraceMethod.TraceMethods[0].TraceMethods.Count);
    }

    [Test]
    public void TestSingleMethodInMultiThreads()
    {
        var threads = new List<Thread>();

        for (int i = 0; i < ThreadsCount; i++)
        {
            var newThread = new Thread(SingleMethod);
            threads.Add(newThread);
            newThread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Assert.AreEqual(ThreadsCount, _tracer.GetTraceResult().Threads.Count);
    }
}