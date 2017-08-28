
namespace CoWorker.LightMvc
{
    using System;
    using System.Linq.Expressions;

    public static class LightMvcHelper
    {
        internal static Expression<Func<TEntity, bool>> EqualWithId<TEntity>(this object val)
            where TEntity : class
          => typeof(TEntity).ToParameter()
           .MakeLambda<Func<TEntity, bool>>(
              exp => exp.GetPropertyOrField("Id")
                  .EqualWith(val.ToConstant()));
    }
}
