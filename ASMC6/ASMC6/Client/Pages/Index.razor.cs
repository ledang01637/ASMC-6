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
using Microsoft.JSInterop;

namespace ASMC6.Client.Pages
{
    public partial class Index
    {
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
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
                }
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
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
