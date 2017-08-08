using System;

namespace VisualPrice_Job.Interface
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }
}
