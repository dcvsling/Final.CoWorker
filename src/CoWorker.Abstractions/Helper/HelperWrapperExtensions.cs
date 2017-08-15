
namespace System
{
    using CoWorker.Abstractions.Helper;

	public static class HelperWrapperExtensions
	{
		public static IObject<T> AsHelper<T>(this T t)
			=> new NonNullObjectWrapper<T>(t);
	}
}
