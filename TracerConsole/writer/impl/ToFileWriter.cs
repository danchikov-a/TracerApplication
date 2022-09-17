namespace TracerConsole.writer.impl;

public class ToFileWriter : IWriter
{
    private const string SAVE_PATH = "D:/123.txt";

    public void Write(StringWriter stringWriter)
    {
        using (StreamWriter writer = new StreamWriter(SAVE_PATH, false))
        {
            writer.WriteLine(stringWriter);
        }
    }
}