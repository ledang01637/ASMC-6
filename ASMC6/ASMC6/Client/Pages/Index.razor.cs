using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using ASMC6.Shared;
using System.Text.Json;

namespace ASMC6.Client.Pages
{
    public partial class Index
    {
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Menu> listMenu = new List<ASMC6.Shared.Menu>();

        protected override async Task OnInitializedAsync()
        {
            await Load();
        }

        private async Task Load()
        {
            try
            {
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                listMenu = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
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
