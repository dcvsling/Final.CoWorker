using System.Security.Cryptography.X509Certificates;
namespace CoWorker.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PropertyAccessor
    {
        public static PropertyAccessor Create<T, TMember>(Expression<Func<T, TMember>> exp)
            => exp.Body is MemberExpression member && member.Member is PropertyInfo property
                ? new PropertyAccessor(property)
                : throw new ArgumentException($"expression should end of property");

        private readonly PropertyInfo _property;
        private Func<object, object> _get;
        private Action<object, object> _set;
        public PropertyAccessor(PropertyInfo property)
        {
            _property = property;
        }


        public object Get(object obj)
            => _get(obj);

        public void Set(object obj, Object val)
            => _set(obj, val);

        private Func<object, object> CreateGetter
        {
            get
            {
                _get = _get ?? typeof(object).ToParameter()
                    .MakeLambda<Func<object, object>>(
                    exp => exp.AsTypeTo(_property.DeclaringType)
                        .GetPropertyOrField(_property.Name)
                        .AsTypeTo(typeof(object))).Compile();
                return _get;
            }
        }

        private Action<object, object> CreateSetter
        {
            get
            {
                _set = _set ?? Enumerable.Repeat(typeof(object), 2).Select(x => x.ToParameter())
                    .MakeLambda<Action<object, object>>(
                    exps => exps.First().AsTypeTo(_property.DeclaringType)
                        .GetPropertyOrField(_property.Name)
                        .AssignFrom(exps.Last())).Compile();
                return _set;
            }
        }
    }
}
