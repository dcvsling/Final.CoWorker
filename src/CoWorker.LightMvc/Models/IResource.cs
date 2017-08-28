
namespace CoWorker.LightMvc.Internal
{
    using System;

    public interface IResource
    {
        Guid Id { get; }
        string FullPath { get; }
        string FileName { get; }
        string Url { get; }
        string ContentType { get; }
        string User { get; }
    }
}
