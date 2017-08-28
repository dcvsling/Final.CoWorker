using CoWorker.EntityFramework.EntityParts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Security.Claims;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    public class ModifierClaimsValueGenerator : IValueGeneratorProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public ModifierClaimsValueGenerator(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public String PropertyName => EntityFrameworkDefault.Modifier;

        public Object Create(EntityEntry entry, IProperty property)
        {
            var http = _accessor.HttpContext;

            return http.User.Identity.IsAuthenticated
                ? http.User.FindFirst(ClaimTypes.Sid).Value
                : http.TraceIdentifier;
        }
    }
}
