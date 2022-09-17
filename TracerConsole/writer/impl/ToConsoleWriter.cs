namespace TracerConsole.writer.impl;

public class ToConsoleWriter : IWriter
{
    public void Write(StringWriter stringWriter)
    {
        Console.WriteLine(stringWriter);
    }
}