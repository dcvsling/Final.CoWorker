
namespace CoWorker.EntityFramework.Abstractions
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;

    public interface IConventionSetFactory
    {
        ConventionSet Create();
    }
}