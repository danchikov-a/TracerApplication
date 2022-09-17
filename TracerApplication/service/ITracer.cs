using TracerApplication.model;

namespace TracerApplication.service;

public interface ITracer
{
    void StartTrace();
    void StopTrace();
    TraceResult GetTraceResult();
}