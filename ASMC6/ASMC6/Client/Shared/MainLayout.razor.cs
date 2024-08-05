using ASMC6.Client.Session;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Threading.Tasks;


namespace ASMC6.Client.Shared
{
    public partial class MainLayout
    {
        private bool IsLoggedIn { get; set; }
        private bool IsRestaurant = true;
        private string UserName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeVoidAsync("checkTokenExpiry");
            var token = await _localStorageService.GetItemAsync("authToken");
            UserName = await _localStorageService.GetItemAsync("userName");
            IsLoggedIn = !string.IsNullOrEmpty(token);
            StateHasChanged();
        }
    }
}
