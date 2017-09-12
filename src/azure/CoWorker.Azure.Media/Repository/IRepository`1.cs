using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.MediaServices.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CoWorker.Azure.Media.Repository
{
    public interface IRepository<TResource> where TResource : class
    {
        Task<TResource> CreateAsync(string name);
        IEnumerable<TResource> Query(Expression<Func<TResource,bool>> predicate);
        Task<TResource> FindAsync(string name);
        Task UpdateAsync(string name);
        Task DeleteAsync(string name);
    }
}
