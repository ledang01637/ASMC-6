using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageRestaurant
    {
        private List<ASMC6.Shared.Restaurant> listRest;
        private ASMC6.Shared.Restaurant rest = new ASMC6.Shared.Restaurant();

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        private async Task Load()
        {
            try
            {
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //private async Task HideProd(int RestaurantId)
        //{
        //    try
        //    {
        //        var restaurant = listRest.FirstOrDefault(p => p.RestaurantId == RestaurantId);
        //        if (restaurant != null)
        //        {
        //            restaurant.IsDelete = true; // Mark the product as deleted
        //            await httpClient.PutAsJsonAsync($"api/Restaurant/UpdateRestaurant/{restaurant.RestaurantId}", restaurant);
        //            // Optionally update the UI to reflect the hidden status
        //            // No need to remove the product from the list
        //            await Load();
        //            StateHasChanged();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error hiding product: {ex.Message}");
        //    }
        //}

        private async Task DeleteProd(int RestaurantId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Restaurant/DeleteRestaurant/{RestaurantId}");
                if (response.IsSuccessStatusCode)
                {
                    listRest = listRest.Where(p => p.RestaurantId != RestaurantId).ToList();
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

        private void EditRest(int restaurantId)
        {
            Navigation.NavigateTo($"/editrestaurant/{restaurantId}");
        }

        private void DeleteRest(int restaurantId)
        {
            Navigation.NavigateTo($"/deleterestaurant/{restaurantId}");
        }


        //chuyển trang
        //protected override void OnInitialized()
        //{
        //    // Automatically redirect after a short delay
        //    Task.Delay(2000).ContinueWith(_ => Navigation.NavigateTo("/", true));
        //}

    }
}
