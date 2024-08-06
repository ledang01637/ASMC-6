using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageProduct
    {
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadProduct();
            isLoaded = true;
        }

        private async Task LoadProduct()
        {
            try
            {
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private async Task HideProd(int productId)
        {
            try
            {
                var product = listProd.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    product.IsDelete = true; // Mark the product as deleted
                    await httpClient.PutAsJsonAsync($"api/Product/UpdateProduct/{productId}", product);
                    // Optionally update the UI to reflect the hidden status
                    // No need to remove the product from the list
                    await LoadProduct();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
            }
        }

        private async Task DeleteProd(int productId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Product/DeleteProduct/{productId}");
                if (response.IsSuccessStatusCode)
                {
                    listProd = listProd.Where(p => p.ProductId != productId).ToList();
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

        private void EditProd(int productId)
        {
            Navigation.NavigateTo("/editproduct/" + productId);
        }
    }
}
