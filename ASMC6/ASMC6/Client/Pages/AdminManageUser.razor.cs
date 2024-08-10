
using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageUser
    {
        private List<ASMC6.Shared.User> listUser = new List<ASMC6.Shared.User>();
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadUser();
            isLoaded = true;
        }

        private async Task LoadUser()
        {
            try
            {
                listUser = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.User>>("api/User/GetUsers");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }


        private async Task HideUser(int userId)
        {
            try
            {
                var user = listUser.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = true; // Mark the user as deleted
                    var response = await httpClient.PutAsJsonAsync($"api/User/{userId}", user);
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadUser();
                        StateHasChanged();
                    }
                    else
                    {
                        Console.WriteLine($"Error hiding user: {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding user: {ex.Message}");
            }
        }

    }
}
