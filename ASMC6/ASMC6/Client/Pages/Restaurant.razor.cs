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

namespace ASMC6.Client.Pages
{
    public partial class Restaurant
    {
        [Parameter]
        public int userId { get; set; }

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
                products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                restaurants = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                menus = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
                categories = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Category>>("api/Category/GetCategories");

                Console.WriteLine("UserID: " + SUser.User.UserId);
                restaurant = restaurants.FirstOrDefault(r => r.UserId == SUser.User.UserId);
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");
            }
        }


        private async Task AddProduct()
        {
            try
            {
                var existingProduct = products.FirstOrDefault(r => r.Name == prodModel.Name);
                if (existingProduct != null)
                {
                    await JS.InvokeVoidAsync("showAlert", "ProductFail");
                    await Task.Delay(1000);
                    return;
                }
                prodModel.Image = imageFileName;
                prodModel.IsDelete = false;
                var response = await httpClient.PostAsJsonAsync("api/Product/AddProduct", prodModel);

                if (response.IsSuccessStatusCode)
                {
                    await JS.InvokeVoidAsync("showAlert", "ProductSuccess");
                    await LoadProducts();
                }
                else
                {
                    Console.WriteLine("Error add product");
                }
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");

            }
        }
        private void OnCategoryChanged(ChangeEventArgs e)
        {
            prodModel.CategoryId = int.Parse(e.Value.ToString());
        }
        private void OnMenuChanged(ChangeEventArgs e)
        {
            prodModel.MenuId = int.Parse(e.Value.ToString());
        }

        private void HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                selectedFile = file;
                imageFileName = file.Name;
                prodModel.Image = imageFileName;
                Console.WriteLine(prodModel.Image);
            }
        }
        private async Task LoadProducts()
        {
            await LoadAll();
            StateHasChanged();
        }

        private async Task LoadProductForEdit(int productId)
        {

            prodModel = await httpClient.GetFromJsonAsync <ASMC6.Shared.Product >($"api/Product/GetProductById/{productId}");
            selectedProductId = productId;

        }
        private async Task UpdateProduct()
        {
            try
            {
                prodModel.Image = imageFileName;
                var response = await httpClient.PutAsJsonAsync($"api/Product/PutProduct/{selectedProductId}", prodModel);

                if (response.IsSuccessStatusCode)
                {
                    await JS.InvokeVoidAsync("showAlert", "ProductSuccess");
                    await LoadProducts();
                }
                else
                {
                    Console.WriteLine("Error update product");
                }
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");
            }
            
        }
        private void ShowConfirmDeleteModal(int productId)
        {
            selectedProductId = productId;
            JS.InvokeVoidAsync("showModal", "ConfirmDeleteModal");
        }
        private async Task DeleteProduct()
        {
            try
            {
                prodModel = await httpClient.GetFromJsonAsync<ASMC6.Shared.Product>($"api/Product/GetProductById/{selectedProductId}");

                prodModel.IsDelete = true;
                var response = await httpClient.PutAsJsonAsync($"api/Product/PutProduct/{selectedProductId}", prodModel);

                if (response.IsSuccessStatusCode)
                {
                    await JS.InvokeVoidAsync("closeModal", "ConfirmDeleteModal");
                    await JS.InvokeVoidAsync("showAlert", "ProductSuccess");
                    await LoadProducts();
                }
                else
                {
                    Console.WriteLine("Error update product");
                }
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");
            }

        }
    }
}
