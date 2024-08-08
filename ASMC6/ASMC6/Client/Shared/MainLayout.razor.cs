using ASMC6.Client.Pages;
using ASMC6.Client.Session;
using ASMC6.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace ASMC6.Client.Shared
{
    public partial class MainLayout
    {
        private bool IsLoggedIn { get; set; }
        private string UserName { get; set; }
        private List<User> users = new List<User>();
        private int RoleId = 0;

        protected override async Task OnInitializedAsync()
        {
            await JS.InvokeVoidAsync("checkTokenExpiry");
            var token = await _localStorageService.GetItemAsync("authToken");
            UserName = await _localStorageService.GetItemAsync("userName");

            var userRoleId = await _localStorageService.GetItemAsync("userRoleId");
            IsLoggedIn = !string.IsNullOrEmpty(token);

            if(!string.IsNullOrEmpty(UserName) ) { await GetUsers(UserName); }
            if (!string.IsNullOrEmpty(userRoleId)) { RoleId = int.Parse(userRoleId); }

            if (SUser.User != null)
            {
                RoleId = SUser.User.RoleId;
            }
            

            StateHasChanged();
        }
        private string GetRestaurantUrl()
        {
             
            return $"/restaurant/{RoleId}";
        }
        private async Task GetUsers(string email)
        {
            try
            {
                users = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");

                if (users != null)
                {
                    SUser.User = users.FirstOrDefault(a => a.Email.Equals(email));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erorr:: " + ex.Message);
            }
        }
    }
}
