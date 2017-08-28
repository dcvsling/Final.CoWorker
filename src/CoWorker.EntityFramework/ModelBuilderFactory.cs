
namespace CoWorker.EntityFramework
{
    using CoWorker.EntityFramework.Abstractions;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ModelBuilderFactory : IModelBuilderFactory
	{
        private readonly IConventionSetFactory _factory;

        public ModelBuilderFactory(IConventionSetFactory factory)
		{
            _factory = factory;
        }
		public ModelBuilder Create(Type type = null)
		{
            var model = new ModelBuilder(_factory.Create());
            if(null != type) model.Entity(type);
			return model;
		}
    }
}
