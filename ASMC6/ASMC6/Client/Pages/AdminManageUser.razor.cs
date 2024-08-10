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
                var user = listUser.FirstOrDefault(p => p.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = true; // Mark the product as deleted
                    await httpClient.PutAsJsonAsync($"api/User/{userId}", user);
                    await LoadUser();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không tìm thấy người dùng : {ex.Message}");
            }
        }

        private async Task RestoreUser(int userId)
        {
            try
            {
                var user = listUser.FirstOrDefault(p => p.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = false; // Restore the product
                    await httpClient.PutAsJsonAsync($"api/User/{userId}", user);
                    await LoadUser();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi khôi phục : {ex.Message}");
            }
        }

        private void EditUser(int userId)
        {
            Navigation.NavigateTo("/edituser/" + userId);
        }
    }
}
