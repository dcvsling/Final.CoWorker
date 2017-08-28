using System.Security.Claims;

namespace CoWorker.LightMvc.Internal
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;

    public class Log
    {
        public static Log Create<TLogType>(HttpContext context)
            => new Log()
            {
                Id = Guid.NewGuid(),
                User = context.User.FindFirst(ClaimTypes.Email).Value,
                Path = context.Request.Path,
                Type = typeof(TLogType).Name,
                Method = context.Request.Method,
                LogTime = DateTimeOffset.Now,
                Properties = {}
            };
        public Guid Id { get; set; }
        public string User { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string Method { get; set; }
        public DateTimeOffset LogTime { get; set; }
        public IDictionary<string, string> Properties { get; } = new Dictionary<string, string>();
    }
}
