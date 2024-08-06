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
        private List<ASMC6.Shared.Order> listOrder;
        private ASMC6.Shared.Order order = new ASMC6.Shared.Order();

        protected override async Task OnInitializedAsync()
        {
            await LoadOrder();
        }

        private async Task LoadOrder()
        {
            try
            {
                listOrder = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Order>>("api/Order/GetOrder");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        //private async Task DeleteOrder(int OrderId)
        //{
        //    try
        //    {
        //        var response = await httpClient.DeleteAsync($"api/Order/DeleteOrder/{OrderId}");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            listOrder = listOrder.Where(p => p.OrderId != OrderId).ToList();
        //            StateHasChanged();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Error deleting product");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error deleting product: {ex.Message}");
        //    }
        //}

        //private void EditOrder(int OrderId)
        //{
        //    Navigation.NavigateTo($"/editorder/{OrderId}");
        //}



        //chuyển trang
        //protected override void OnInitialized()
        //{
        //    // Automatically redirect after a short delay
        //    Task.Delay(2000).ContinueWith(_ => Navigation.NavigateTo("/", true));
        //}

    }
}
