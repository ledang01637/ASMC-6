using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageRestaurant
    {
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Restaurant> filteredRest = new List<ASMC6.Shared.Restaurant>();
        private bool isLoading = true;
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        private async Task Load()
        {
            try
            {
                isLoading = true;
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                filteredRest = listRest;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error: {ex.Message}";
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task HideRest(int restaurantId)
        {
            try
            {
                var restaurant = listRest.FirstOrDefault(p => p.RestaurantId == restaurantId);
                if (restaurant != null)
                {
                    restaurant.IsDelete = !restaurant.IsDelete; // Toggle the delete status
                    await httpClient.PutAsJsonAsync($"api/Restaurant/{restaurantId}", restaurant);
                    await Load();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error hiding product: {ex.Message}";
            }
        }

        private void EditRest(int restaurantId)
        {
            Navigation.NavigateTo($"/editrestaurant/{restaurantId}");
        }

        private void FilterRestaurant(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString().ToLower();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredRest = listRest;
            }
            else if (listRest.Any(p => p.Name.ToLower().Equals(searchTerm)))
            {
                filteredRest = listRest.Where(p => p.Name.ToLower().Equals(searchTerm)).ToList();
            }
            else
            {
                filteredRest = listRest.Where(p => searchTerm.Any(c => p.Name.ToLower().Contains(c))).ToList();
            }
        }




    }
}
