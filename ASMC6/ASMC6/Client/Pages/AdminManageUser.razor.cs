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

        private async Task HideProd(int userId)
        {
            try
            {
                var user = listUser.FirstOrDefault(p => p.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = true; // Mark the product as deleted
                    await httpClient.PutAsJsonAsync($"api/Product/UpdateProduct/{userId}", user);
                    // Optionally update the UI to reflect the hidden status
                    // No need to remove the product from the list
                    await LoadUser();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
            }
        }

        private async Task DeleteProd(int userId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/User/DeleteUser/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    listUser = listUser.Where(p => p.UserId != userId).ToList();
                    StateHasChanged();
                }
                else
                {
                    Console.WriteLine("Error deleting product");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
            }
        }

        private void EditProd(int userId)
        {
            Navigation.NavigateTo("/edituser/" + userId);
        }
    }
}
