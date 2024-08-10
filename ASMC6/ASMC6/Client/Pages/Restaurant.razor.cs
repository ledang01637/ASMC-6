using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http;

namespace ASMC6.Client.Pages
{
    public partial class Restaurant
    {
        [Parameter]
        public int userId { get; set; }

        private List<ASMC6.Shared.Restaurant> restaurants = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> products = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Menu> menus = new List<ASMC6.Shared.Menu>();
        private ASMC6.Shared.Restaurant restaurant = new ASMC6.Shared.Restaurant();
        protected override async Task OnInitializedAsync()
        {
           await LoadAll();
        }

        private async Task LoadAll()
        {
            try
            {
                products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                restaurants = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                menus = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
                restaurant = restaurants.FirstOrDefault(r => r.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
