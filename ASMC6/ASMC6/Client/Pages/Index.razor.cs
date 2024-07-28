using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace ASMC6.Client.Pages
{
    public partial class Index
    {
        private List<ASMC6.Shared.Product> listProd;
        private List<ASMC6.Shared.Restaurant> listRest;
        private ASMC6.Shared.Product prod = new ASMC6.Shared.Product();

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //private async Task AddProd()
        //{
        //    await httpClient.PostAsJsonAsync("api/Product/AddProduct", prod);
        //    await Load();
        //    StateHasChanged();
        //}
        private async Task AddToCart(ASMC6.Shared.Product product)
        {
            await CartService.AddItemToCartAsync(product);
            Navigation.NavigateTo("/cart");
        }
    }
}
