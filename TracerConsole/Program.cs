using TracerApplication.serializer.impl;
using TracerApplication.service.impl;
using TracerConsole.serializer;
using TracerConsole.serializer.impl;
using TracerConsole.writer;
using TracerConsole.writer.impl;

namespace TracerConsole;

class Program
{
    private static readonly Tracer _tracer = new();
    private static ISerializer _serializer;
    private static IWriter _writer;
    
    public static void Main()
    {
        var thread1 = new Thread(Method1);
        var thread2 = new Thread(Method1);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        RecordData(new ToJsonSerializer());
        RecordData(new ToXmlSerializer());
    }

    private static void RecordData(ISerializer serializer)
    {
        _serializer = serializer;
        var jsonStringWriter = _serializer.Serialize(_tracer.GetTraceResult());

        _writer = new ToConsoleWriter();
        _writer.Write(jsonStringWriter);

        _writer = new ToFileWriter();
        _writer.Write(jsonStringWriter);
    }

    private static void Method1()
    {
        _tracer.StartTrace();
        Method2();
        Method3();
        Thread.Sleep(45);
        _tracer.StopTrace();
    }

    private static void Method2()
    {
        _tracer.StartTrace();
        Method4();
        Method5();
        Thread.Sleep(31);
        _tracer.StopTrace();
    }

    private static void Method3()
    {
        _tracer.StartTrace();
        Thread.Sleep(12);
        _tracer.StopTrace();
    }

    private static void Method4()
    {
        _tracer.StartTrace();
        Thread.Sleep(21);
        _tracer.StopTrace();
    }
    
    private static void Method5()
    {
        _tracer.StartTrace();
        Method6();
        Thread.Sleep(24);
        _tracer.StopTrace();
    }
    
    private static void Method6()
    {
        _tracer.StartTrace();
        Thread.Sleep(29);
        _tracer.StopTrace();
    }
}