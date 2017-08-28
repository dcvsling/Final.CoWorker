
namespace CoWorker.LightMvc.Internal
{
    using System;

    public class Resource : IResource
    {
        public Guid Id { get; set; }

        public String FullPath { get; set; }

        public String FileName { get; set; }

        public String Url { get; set; }

        public String ContentType { get; set; }
        public string User { get; set; }
    }
}
