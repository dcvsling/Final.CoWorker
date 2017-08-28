using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
namespace CoWorker.EntityFramework.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using CoWorker.Builder;
    using System.Threading.Tasks;
    using CoWorker.EntityFramework.Internal;
    using System.IO;
    using CoWorker.DependencyInjection.Factory;
    using Microsoft.EntityFrameworkCore.Design;

    public class RepositoryProviderTests
    {
        private ServiceProvider Service =>
            new ServiceCollection()
                .AddLogging()
                .AddSingleton(Mock.Of<IHostingEnvironment>(
                    env  => env.EnvironmentName == EnvironmentName.Development
                    && env.ContentRootPath == Directory.GetCurrentDirectory()
                    && env.ContentRootFileProvider == new PhysicalFileProvider(Directory.GetCurrentDirectory())))
                .AddDefaultService()
                .AddEntityFrameworkService()
                .BuildServiceProvider();
        
        [Fact]
        async public Task test_RepositoryProvider()
        {
            using(var provider = Service)
            {
                var factory = provider.GetRequiredService<IDesignTimeDbContextFactory<DbContext<ModelB>>>();
                using (var context = factory.CreateDbContext(new string[] { }))
                {
                    await context.AddAsync(new ModelB());
                    await context.SaveChangesAsync();
                    var result = await context.Set<ModelB>().ToListAsync();
                    Assert.Single(result);
                }
            }
        }
        
        [Fact]
        public void test_DataSourceConfigureOptions()
        {
            var service = Service;
            var actual = service.GetService<IOptionsCache<DataSource>>().Get();
            Assert.Equal("esport-asia-db", actual.Name);
        }
    }
}