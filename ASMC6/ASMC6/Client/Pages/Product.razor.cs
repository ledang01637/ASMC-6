using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class Product
    {

        private List<ASMC6.Shared.Product> listProd;
        private List<ASMC6.Shared.Product> listProd2;
        private List<ASMC6.Shared.Product> pagedProducts;
        private ASMC6.Shared.Product prod;
        private decimal minPrice;
        private decimal maxPrice = 100000;
        [Parameter] public int RestaurantId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
            ApplyFilter();
        }
        private async Task LoadProducts()
        {
            try
            {
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/product/GetProducts");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task AddProd()
        {
            await httpClient.PostAsJsonAsync("api/Product/AddProduct", prod);
            await LoadProducts();
            StateHasChanged();
        }
        private async Task AddToCart(ASMC6.Shared.Product product)
        {
            await CartService.AddItemToCartAsync(product);
            Navigation.NavigateTo("/cart");
        }


        private void ApplyFilter()
        {
            if (listProd != null)
            {
                pagedProducts = listProd
                    .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                    .ToList();
            }
        }

        private void FilterByPrice()
        {
            if (minPrice < 0) minPrice = 0;
            if (maxPrice < 0) maxPrice = 100000;
            ApplyFilter();
            StateHasChanged();
        }

        private void ResetPriceFilter()
        {
            minPrice = 0;
            maxPrice = decimal.MaxValue;
            ApplyFilter();
            StateHasChanged();
        }
    }
}
