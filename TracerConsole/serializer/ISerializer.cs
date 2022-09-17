using TracerApplication.model;

namespace TracerConsole.serializer;

public interface ISerializer
{
    public StringWriter Serialize(TraceResult traceResult);
}