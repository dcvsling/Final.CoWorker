namespace CoWorker.Builder
{
    using Microsoft.Extensions.DependencyInjection;
	using System;
    using System.Collections.Generic;
    using System.Linq;

    public delegate TBuilder ConfigureDelegate<TBuilder>(TBuilder builder);
}