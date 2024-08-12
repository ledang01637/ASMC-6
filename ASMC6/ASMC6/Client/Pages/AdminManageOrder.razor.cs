
using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using ASMC6.Shared;
using ASMC6.Client.Session;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageOrder
    {
        private List<ASMC6.Shared.Order> listOrder = new List<ASMC6.Shared.Order>();
        private List<ASMC6.Shared.OrderItem> listOrderItem = new List<ASMC6.Shared.OrderItem>();
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Menu> listMenu = new List<ASMC6.Shared.Menu>();
        private List<ASMC6.Shared.User> listUser = new List<ASMC6.Shared.User>();

        private ASMC6.Shared.Restaurant restaurants = new ASMC6.Shared.Restaurant();
        private ASMC6.Shared.Product products = new ASMC6.Shared.Product();
        private ASMC6.Shared.Order orders = new ASMC6.Shared.Order();
        private ASMC6.Shared.Category categories = new ASMC6.Shared.Category();
        private ASMC6.Shared.User users = new ASMC6.Shared.User();

        private List<ASMC6.Shared.Product> originalListProd;

        private bool isLoaded = false;
        private string errorMessage;


        protected override async Task OnInitializedAsync()
        {
            await LoadOrder();
            isLoaded = true;
        }


        private async Task LoadOrder()
        {
            try
            {
                listOrder = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Order>>("api/Order/GetOrders");
                listOrderItem = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.OrderItem>>("api/OrderItem/GetOrderItems");
                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                listMenu = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");
                listUser = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.User>>("api/User/GetUsers");

                restaurants = await httpClient.GetFromJsonAsync<ASMC6.Shared.Restaurant>("api/Restaurant/GetRestaurants");
                products = await httpClient.GetFromJsonAsync<ASMC6.Shared.Product>("api/Product/GetProducts");
                orders = await httpClient.GetFromJsonAsync<ASMC6.Shared.Order>("api/Order/GetOrders");
                categories = await httpClient.GetFromJsonAsync<ASMC6.Shared.Category>("api/Category/GetCategories");

                listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                originalListProd = new List<ASMC6.Shared.Product>(listProd);

                //listRest = restaurants.FirstOrDefault(r => r.UserId == SUser.User.UserId);
            }
            catch (Exception ex)
            {
                errorMessage = $"Error loading orders: {ex.Message}";
            }
        }



        private async Task HideProd(int orderId)
        {
            try
            {
                var order = listOrder.FirstOrDefault(p => p.OrderId == orderId);
                if (order != null)
                {
                    // Assuming you want to mark the order as hidden
                    await httpClient.PutAsJsonAsync($"api/Order/UpdateOrder/{orderId}", order);
                    await LoadOrder();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error hiding order: {ex.Message}";
            }
        }

        private async Task DeleteOrder(int orderId)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"api/Order/DeleteOrder/{orderId}");
                if (response.IsSuccessStatusCode)
                {
                    listOrder = listOrder.Where(p => p.OrderId != orderId).ToList();
                    StateHasChanged();
                }
                else
                {
                    errorMessage = "Error deleting order";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error deleting order: {ex.Message}";
            }
        }

        private void EditOrder(int orderId)
        {
            Navigation.NavigateTo($"/editorder/{orderId}");
        }

        private void SearchName(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString().ToLower();
            listProd = listProd.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm)).ToList();
        }




    }
}

