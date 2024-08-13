
using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Components;
using ASMC6.Shared;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageUser
    {
        private List<ASMC6.Shared.User> listUser = new List<ASMC6.Shared.User>();
        private List<ASMC6.Shared.User> filteredUser = new List<ASMC6.Shared.User>();
        private bool isLoaded = false;
        private string errorMessage;

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
                filteredUser = listUser;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error loading users: {ex.Message}";
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
                        errorMessage = $"Error hiding user: {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error hiding user: {ex.Message}";
            }
        }

        private async Task RestoreUser(int userId)
        {
            try
            {
                var user = listUser.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.IsDelete = false; // Restore the user
                    var response = await httpClient.PutAsJsonAsync($"api/User/{userId}", user);
                    if (response.IsSuccessStatusCode)
                    {
                        await LoadUser();
                        StateHasChanged();
                    }
                    else
                    {
                        errorMessage = $"Error restoring user: {response.ReasonPhrase}";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error restoring user: {ex.Message}";
            }
        }

        private void FilterUser(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString().ToLower();
            filteredUser = string.IsNullOrWhiteSpace(searchTerm)
                ? listUser
                : listUser.Where(u => u.Name.ToLower().Contains(searchTerm) || u.Email.ToLower().Contains(searchTerm)).ToList();
        }



    }
}
