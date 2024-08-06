using ASMC6.Client.Session;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;


namespace ASMC6.Client.Shared
{
    public partial class MainLayout
    {
        private bool IsLoggedIn { get; set; }
        private string UserName { get; set; }
        private int userId = 0;

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeVoidAsync("checkTokenExpiry");
            var token = await _localStorageService.GetItemAsync("authToken");
            UserName = await _localStorageService.GetItemAsync("userName");
            var userRoleId = await _localStorageService.GetItemAsync("userRoleId");
            IsLoggedIn = !string.IsNullOrEmpty(token);

            if (userRoleId != null)
            userId = int.Parse(userRoleId);

            if (SUser.User != null)
            {
                userId = SUser.User.UserId;
            }

            StateHasChanged();
        }
        private string GetRestaurantUrl()
        {
            return $"/restaurant/{userId}";
        }
    }
}
