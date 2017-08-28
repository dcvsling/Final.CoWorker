using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
namespace CoWorker.LightMvc
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMvcLight(this IApplicationBuilder app)
            => app.UseMvc();
    }
}
