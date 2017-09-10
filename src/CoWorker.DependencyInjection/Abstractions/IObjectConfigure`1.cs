namespace CoWorker.DependencyInjection.Factory
{

    public interface IObjectConfigure<T> where T : class
    {
        void Configure(string name, T options);
    }
}