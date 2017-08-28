using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CoWorker.EntityFramework.EntityParts.ValueGenerators
{
    using System;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore;

    public class CreatorClaimsValueGenerator : IValueGeneratorProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public CreatorClaimsValueGenerator(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public String PropertyName => EntityFrameworkDefault.Creator;

        public Object Create(EntityEntry entry, IProperty property)
            => EntityState.Detached == entry.State
                ? OnAdd(_accessor.HttpContext)
                : OnUpdate(entry, property);

        private string OnAdd(HttpContext context)
            => context.User.Identity.IsAuthenticated
                ? context.User.FindFirst(ClaimTypes.Sid).Value
                : context.TraceIdentifier;

        private string OnUpdate(EntityEntry entry, IProperty property)
            => entry.GetDatabaseValues().GetValue<string>(property);
    }
}
