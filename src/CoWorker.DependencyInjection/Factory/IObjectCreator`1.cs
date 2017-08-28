namespace CoWorker.DependencyInjection.Factory
{

    public interface IObjectCreator<T> where T : class
    {
        T Create(string name = null);
    }
}