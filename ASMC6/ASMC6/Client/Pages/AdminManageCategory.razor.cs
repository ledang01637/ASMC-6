using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using ASMC6.Shared;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageCategory
    {
        private List<ASMC6.Shared.Category> listCategory = new List<ASMC6.Shared.Category>();
        private List<ASMC6.Shared.Category> filteredCategory = new List<ASMC6.Shared.Category>();
        //private List<Category> categories = new List<Category>();
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            isLoaded = true;
        }

        private async Task LoadCategories()
        {
            try
            {
                listCategory = await httpClient.GetFromJsonAsync<List<Category>>("api/Category/GetCategory");
                filteredCategory = listCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading categories: {ex.Message}");
            }
        }

        //private async Task HideProd(int CategoryId)
        //{
        //    try
        //    {
        //        var product = listProd.FirstOrDefault(p => p.ProductId == productId);
        //        if (product != null)
        //        {
        //            product.IsDelete = true; // Mark the product as deleted
        //            await httpClient.PutAsJsonAsync($"api/Product/{productId}", product);
        //            await LoadProduct();
        //            StateHasChanged();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error hiding product: {ex.Message}");
        //    }
        //}


        //private void EditProd(int productId)
        //{
        //    Navigation.NavigateTo("/editproduct/" + productId);
        //}

        //private void FilterProducts(ChangeEventArgs e)
        //{
        //    var searchTerm = e.Value.ToString().ToLower();
        //    filteredProd = listProd.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm)).ToList();
        //}

        //private void FilterByPrice(ChangeEventArgs e)
        //{
        //    if (decimal.TryParse(e.Value.ToString(), out decimal searchPrice))
        //    {
        //        filteredProd = listProd.Where(p => p.Price >= 0 && p.Price <= searchPrice).ToList();
        //    }
        //    else
        //    {
        //        filteredProd = listProd;
        //    }
        //}


        //chuyển trang
        //protected override void OnInitialized()
        //{
        //    // Automatically redirect after a short delay
        //    Task.Delay(2000).ContinueWith(_ => Navigation.NavigateTo("/", true));
        //}

    }
}
