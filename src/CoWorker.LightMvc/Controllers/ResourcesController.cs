using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using CoWorker.LightMvc.Internal;
using CoWorker.Net.FileUpload;

namespace CoWorker.LightMvc.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class ResourcesController<TEntity> where TEntity : class
    {
        private readonly FileUploadHandler _upload;
        private readonly IResourceRepository<TEntity> _resources;
        private readonly IHttpContextAccessor _accessor;

        public ResourcesController(FileUploadHandler upload,IResourceRepository<TEntity> resources,IHttpContextAccessor accessor)
        {
            this._upload = upload;
            this._resources = resources;
            this._accessor = accessor;
        }

        public Task<IEnumerable<IResource>> Get()
            => _resources.List;
        public Task<IResource> Get(Guid id)
            => _resources.Get(id);

        async public Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            files.Where(x => x.Length > 0)
                .Select(async x => await WriteFile(x, filePath))
                .ToArray();
            var context = _accessor.HttpContext;

            return new OkObjectResult(
            new {
                count = files.Count,
                size,
                filePath,
                resources = files.Select(x => CreateResource(context,x,filePath)).ToArray()
            });
        }

        async public Task<IActionResult> Put(Guid id)
        {
            await _resources.Restore(id);
            var current = await _resources.Get();
            return new AcceptedResult(current.Url, current);
        }

        async public Task<IActionResult> Delete()
        {
            await _resources.DropLast();
            var current = await _resources.Get();
            return new AcceptedResult(current.Url,current);
        }

        async public Task WriteFile(IFormFile file,string path)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private IResource CreateResource(HttpContext context,IFormFile formfile,string path)
            => new Resource()
            {
                ContentType = formfile.ContentType,
                Url = context.Request.Path,
                FullPath = path,
                FileName = formfile.FileName,
                Id = Guid.NewGuid(),
                User = context.User.FindFirst(ClaimTypes.Name).Value
            };
    }
}