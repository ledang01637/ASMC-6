
using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using System.Linq;
using ASMC6.Shared;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageOrder
    {
        private List<ASMC6.Shared.Order> listOrder = new List<ASMC6.Shared.Order>();
        private List<ASMC6.Shared.OrderItem> listOrderItem = new List<ASMC6.Shared.OrderItem>();
        private List<ASMC6.Shared.Product> listProd = new List<ASMC6.Shared.Product>();
        private List<ASMC6.Shared.Restaurant> listRest = new List<ASMC6.Shared.Restaurant>();
        private List<ASMC6.Shared.Menu> listMenu = new List<ASMC6.Shared.Menu>();
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
                //listOrderItem = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.OrderItem>>("api/OrderItem/GetOrderItems");
                //listProd = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                //listRest = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Restaurant>>("api/Restaurant/GetRestaurants");
                //listMenu = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Menu>>("api/Menu/GetMenus");

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
    }
}

