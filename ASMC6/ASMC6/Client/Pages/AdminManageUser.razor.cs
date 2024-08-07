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

        private async Task DeleteUser(int userId)
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
                        // Optionally refresh the list or update the UI to reflect the hidden status
                        StateHasChanged();
                    }
                    else
                    {
                        Console.WriteLine("Error updating user");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
            }
        }


        //private async Task DeleteUser(int UserId)
        //{
        //    try
        //    {
        //        var response = await httpClient.DeleteAsync($"api/Restaurant/{UserId}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            listUser = listUser.Where(p => p.UserId != UserId).ToList();
        //            StateHasChanged();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error deleting product");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deleting product: {ex.Message}");
        //    }
        //}

        private async Task EditProdAsync(int userId)
        {
            try
            {
                var user = listUser.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = false; // Mark the user as deleted
                    var response = await httpClient.PutAsJsonAsync($"api/User/{userId}", user);
                    if (response.IsSuccessStatusCode)
                    {
                        // Optionally refresh the list or update the UI to reflect the hidden status
                        StateHasChanged();
                    }
                    else
                    {
                        Console.WriteLine("Error updating user");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
            }
        }
    }
}
