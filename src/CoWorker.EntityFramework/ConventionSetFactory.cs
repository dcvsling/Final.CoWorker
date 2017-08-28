using Microsoft.EntityFrameworkCore;
using CoWorker.EntityFramework.Abstractions;
namespace CoWorker.EntityFramework.Conventions
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
    using Microsoft.EntityFrameworkCore.Metadata.Conventions;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;

    public class ConventionSetFactory : IConventionSetFactory
    {
        private readonly ICoreConventionSetBuilder _corebuilder;
        private readonly IEnumerable<IConfigureOptions<ConventionSet>> _conventions;
        private ConventionSet _conventionSet;

        public ConventionSetFactory(
            ICoreConventionSetBuilder corebuilder,
            IEnumerable<IConfigureOptions<ConventionSet>> conventions)
        {
            _corebuilder = corebuilder;
            _conventions = conventions;
        }

        public ConventionSet Create()
            => _conventions.Aggregate(
                _corebuilder.CreateConventionSet(),
                (seed,next) => next.Configure(seed));
    }
}