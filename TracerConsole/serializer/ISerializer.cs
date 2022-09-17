using TracerApplication.model;

namespace TracerConsole.serializer;

public interface ISerializer
{
    public void Serialize(TraceResult traceResult);
}