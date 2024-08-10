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

                restaurant = restaurants.FirstOrDefault(r => r.UserId == SUser.User.UserId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private async Task AddProduct()
        {
            try
            {
                prodModel.Image = imageFileName;
                prodModel.IsDelete = false;
                var response = await httpClient.PostAsJsonAsync("api/Product/AddProduct", prodModel);

                if (response.IsSuccessStatusCode)
                {
                    await JS.InvokeVoidAsync("showAlert", "AddProduct");
                    await LoadProducts();
                }
                else
                {
                    Console.WriteLine("Error add product");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");

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

    }
}
