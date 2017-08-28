namespace CoWorker.Identity
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	public class IdentityDbContext : IdentityDbContext<User,Role,Guid>
	{
		
		public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
		{
			
		}
	}
}
