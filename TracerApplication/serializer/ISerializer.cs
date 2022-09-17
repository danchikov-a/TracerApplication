using TracerApplication.model;

namespace TracerApplication.serializer;

public interface ISerializer
{
    public void Serialize(TraceResult traceResult);
}