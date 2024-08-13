using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Net.Http;
using ASMC6.Client.Session;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;
using ASMC6.Shared;
using ASMC6.Server.Service;

namespace ASMC6.Client.Pages
{
    public partial class RestaurantProduct
    {
        [Parameter]
        public int restaurantId { get; set; }

        private List<ASMC6.Shared.Product> pagedProducts;
        private List<ASMC6.Shared.Menu> Menu;
        private List<ASMC6.Shared.Restaurant> restaurants = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Product> products = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Menu> menus = new List<ASMC6.Shared.Menu>();
        private List<ASMC6.Shared.Category> categories = new List<ASMC6.Shared.Category>();
        private ASMC6.Shared.Restaurant restaurant = new ASMC6.Shared.Restaurant();
        public ASMC6.Shared.Product prodModel = new ASMC6.Shared.Product();
        private string imageFileName;
        private IBrowserFile selectedFile;
        private int selectedProductId;

        protected override async Task OnInitializedAsync()
        {
            await LoadAll();
        }

        private async Task LoadAll()
        {
            try
            {
                var restaurantRespone = await httpClient.GetFromJsonAsync<ASMC6.Shared.Restaurant>($"api/Restaurant/{restaurantId}");
                if (restaurantRespone != null)
                {
                    restaurant = restaurantRespone;
                    menus = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
                    restaurants = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                    categories = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Category>>("api/Category/GetCategories");
                    products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy cửa hàng");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoadProducts()
        {

            await LoadAll();
            StateHasChanged();
        }
        private async Task AddToCart(ASMC6.Shared.Product product)
        {
            await CartService.AddItemToCartAsync(product);
            Navigation.NavigateTo("/cart");
        }
    }
}
