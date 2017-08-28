using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoWorker.Security.Authentication
{
    public interface IAuthorizationHandler
    {
        Task<IEnumerable<AuthenticationScheme>> ListScheme { get; }
        Task<Dictionary<System.String, System.String>> User { get; }

        Task ChallengeAsync(System.String scheme = null);
        Task Login();
        Task Logout();
    }
}