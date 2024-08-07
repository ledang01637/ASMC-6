using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageRestaurant
    {
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadRestaurant();
            isLoaded = true;
        }

        private async Task LoadRestaurant()
        {
            try
            {
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private async Task HideProd(int restaurantId)
        {
            try
            {
                var rests = listRest.FirstOrDefault(p => p.RestaurantId == restaurantId);
                if (rests != null)
                {
                    rests.IsDelete = true; // Mark the product as deleted
                    await httpClient.PutAsJsonAsync($"api/Restaurant/{restaurantId}", rests);
                    await LoadRestaurant();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
            }
        }

        private async Task DeleteProd(int restaurantId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Restaurant/{restaurantId}");
                if (response.IsSuccessStatusCode)
                {
                    listRest = listRest.Where(p => p.RestaurantId != restaurantId).ToList();
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


        private void EditProd(int restaurantId)
        {
            Navigation.NavigateTo("/editrestaurant/" + restaurantId);
        }
    }
}
