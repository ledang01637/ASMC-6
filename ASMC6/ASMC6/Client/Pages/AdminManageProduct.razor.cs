﻿using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using Microsoft.AspNetCore.Components;
using ASMC6.Shared;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageProduct
    {
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Product> filteredProd = new List<ASMC6.Shared.Product>();
        private List<Category> categories = new List<Category>();
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadProduct();
            await LoadCategories();
            isLoaded = true;
        }

        private async Task LoadProduct()
        {
            try
            {
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                filteredProd = listProd;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private async Task LoadCategories()
        {
            try
            {
                categories = await httpClient.GetFromJsonAsync<List<Category>>("api/Category/GetCategories");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
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
                    await httpClient.PutAsJsonAsync($"api/Product/{productId}", product);
                    await LoadProduct();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
            }
        }

        private async Task RestoreProd(int productId)
        {
            try
            {
                var product = listProd.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    product.IsDelete = false; // Restore the product
                    await httpClient.PutAsJsonAsync($"api/Product/{productId}", product);
                    await LoadProduct();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Đã xảy ra lỗi khi khôi phục : {ex.Message}");
            }
        }
   

    //private async Task DeleteProd(int productId)
    //    {
    //        try
    //        {
    //            var response = await httpClient.DeleteAsync($"api/Product/DeleteProduct/{productId}");
    //            if (response.IsSuccessStatusCode)
    //            {
    //                listProd = listProd.Where(p => p.ProductId != productId).ToList();
    //                filteredProd = listProd;
    //                StateHasChanged();
    //            }
    //            else
    //            {
    //                Console.WriteLine("Error deleting product");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Error deleting product: {ex.Message}");
    //        }
    //    }

        private void EditProd(int productId)
        {
            Navigation.NavigateTo("/editproduct/" + productId);
        }

        private void FilterProducts(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString().ToLower();
            filteredProd = listProd.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm)).ToList();
        }

        private void FilterByPrice(ChangeEventArgs e)
        {
            if (decimal.TryParse(e.Value.ToString(), out decimal searchPrice))
            {
                filteredProd = listProd.Where(p => p.Price >= 0 && p.Price <= searchPrice).ToList();
            }
            else
            {
                filteredProd = listProd;
            }
        }
    }
}
