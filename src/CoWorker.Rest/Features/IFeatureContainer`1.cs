namespace CoWorker.Rest.ApplicationParts
{
    using System;
    using System.Collections.Generic;

    public interface IFeatureContainer<TFeature>
	{
		Func<TFeature,bool> IsEnable { get; }
		IList<TFeature> Features { get; }
	}
}
