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



        private async Task HideRest(int restaurantId)
        {
            try
            {
                var restaurant = listRest.FirstOrDefault(p => p.RestaurantId == restaurantId);
                if (restaurant != null)
                {
                    restaurant.IsDelete = true; // Mark the product as deleted
                    await httpClient.PutAsJsonAsync($"api/Restaurant/{restaurantId}", restaurant);
                    await Load();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
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
