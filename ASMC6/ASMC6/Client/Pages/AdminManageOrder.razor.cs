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
        private bool isLoaded = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadOrder();
            isLoaded = true;
        }

        private async Task LoadOrder()
        {
            try
            {
                listOrder = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Order>>("api/Order/GetOrder");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private async Task HideProd(int orderId)
        {
            try
            {
                var order = listOrder.FirstOrDefault(p => p.OrderId == orderId);
                if (order != null)
                {
                    await httpClient.PutAsJsonAsync($"api/Order/UpdateOrder/{orderId}", order);
                    // Optionally update the UI to reflect the hidden status
                    // No need to remove the product from the list
                    await LoadOrder();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error hiding product: {ex.Message}");
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
                    Console.WriteLine("Error deleting Order");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
            }
        }

        private void EditOrder(int orderId)
        {
            Navigation.NavigateTo("/editorder/" + orderId);
        }

    }
}
