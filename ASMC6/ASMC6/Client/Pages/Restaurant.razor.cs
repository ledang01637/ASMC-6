using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace ASMC6.Client.Pages
{
    public partial class Restaurant
    {
        [Parameter]
        public int userId { get; set; }

        private List<ASMC6.Shared.Restaurant> restaurants = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> products = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Product> listProMenu = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Menu> menus = new List<ASMC6.Shared.Menu>();
        private ASMC6.Shared.Restaurant restaurant = new ASMC6.Shared.Restaurant();
        protected override async Task OnInitializedAsync()
        {
            await LoadRestaurants();
            await LoadMenus();
            await LoadProducts();
            LoadAll();
        }

        private void LoadAll()
        {
            if(restaurant != null)
            {
                var menuRes = menus.Where(a => a.RestaurantId == restaurant.RestaurantId).ToList();
                foreach (var menu in menuRes)
                {
                    listProMenu = products.Where(p => p.MenuId == menu.MenuId).ToList();
                }
                Console.WriteLine(listProMenu);
            }
        }

        private async Task LoadRestaurants()
        {
            try
            {
                restaurants = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                restaurant = restaurants.FirstOrDefault(r => r.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task LoadProducts()
        {
            try
            {
                products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Restaurant/GetRestaurants");
                restaurant = restaurants.FirstOrDefault(r => r.UserId == userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task LoadMenus()
        {
            try
            {
                menus = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
