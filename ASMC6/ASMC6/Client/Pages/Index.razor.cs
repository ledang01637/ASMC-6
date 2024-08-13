using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using ASMC6.Shared;
using System.Text.Json;
using ASMC6.Client.Session;

namespace ASMC6.Client.Pages
{
    public partial class Index
    {
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Product> products = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Menu> listMenu = new List<ASMC6.Shared.Menu>();
        private ASMC6.Shared.Restaurant restaurant = new ASMC6.Shared.Restaurant();

        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }

        private async Task Load()
        {
            try
            {
                products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                listMenu = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
                if (SUser.User != null)
                {
                    if(SUser.User.RoleId == 2)
                    {
                        restaurant = listRest.FirstOrDefault(r => r.UserId == SUser.User.UserId);
                        var listRestLogin = listRest.Where(m => m.RestaurantId != restaurant.RestaurantId).ToList();
                        var menuForRest = listMenu.Where(menu => listRestLogin.Any(lr => lr.RestaurantId == menu.RestaurantId)).ToList();
                        listProd = listProd.Where(p => menuForRest.Any(mr => mr.MenuId == p.MenuId)).ToList();
                    }
                    if (restaurant != null)
                    {
                        listRest = listRest.Where(a => a.RestaurantId != restaurant.RestaurantId).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async Task AddToCart(ASMC6.Shared.Product product)
        {
            await CartService.AddItemToCartAsync(product);
            Navigation.NavigateTo("/cart");
        }
    }
}
