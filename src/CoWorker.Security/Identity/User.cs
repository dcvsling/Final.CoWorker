
namespace CoWorker.Identity
{
	using System;

	using Microsoft.AspNetCore.Identity;
    
    public abstract class User : IdentityUser<Guid>
    {
    }
}
