using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace CoWorker.EntityFramework.Tests
{
    public interface IEntity { }
    public class ModelA : IEntity
    {
        public Guid Id { get; set; }
        public string Test { get; set; } = "A";
        public ModelB B { get; set; }
    }
    public class ModelB : IEntity
    {
        public Guid Id { get; set; }
        public string Test { get; set; } = "B";
        public IEnumerable<ModelC> Cs { get; set; }
    }
    public class ModelC : IEntity
    {
        public Guid Id { get; set; }
        public string testC { get; set; } = "C";
    }

    public class DbContext<TContext> : DbContext where  TContext:class
    {
        protected DbContext() : base()
        {
        }

        public DbContext(DbContextOptions<DbContext<TContext>> options) : base(options)
        {
        }
    }
}