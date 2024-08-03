using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASM6.Admin.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        //private readonly ClaimsPrincipal _principal = new ClaimsPrincipal(new ClaimsIdentity);

        public CustomAuthentication(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new System.NotImplementedException();
        }
        //public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        //{
        //    new 
        //}
    }
}
