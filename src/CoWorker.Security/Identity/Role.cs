
namespace CoWorker.Identity
{
	using System;
	using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole<Guid>
	{
        public List<Claim> Claims { get; set; }
	}
}
