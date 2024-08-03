using ASMC6.Client.Session;
using ASMC6.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ASMC6.Client.Pages
{
    public partial class PurchaseHistory
    {
        private List<Order> orders;
        private List<ASMC6.Shared.Product> products;
        private List<OrderItem> orderItems;
        private List<Order> orderUs;
        protected override async Task OnInitializedAsync()
        {
            await LoadHistories();
        }

        private async Task LoadHistories()
        {
            try
            {
                orders = await httpClient.GetFromJsonAsync<List<Order>>("api/Order/GetOrders");
                products = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Product>>("api/Product/GetProducts");
                orderItems = await httpClient.GetFromJsonAsync<List<OrderItem>>("api/OrderItem/GetOrderItems");

                if (orders != null)
                {
                    orderUs = orders.Where(a => a.UserId == SUser.User.UserId).ToList();
                }

                if (orderItems != null && products != null)
                {
                    foreach (var item in orderItems)
                    {
                        var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                        if (product != null)
                        {
                            product.OrderItems = orderItems.Where(oi => oi.ProductId == product.ProductId).ToList();
                        }
                    }
                }
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu history: {ex.Message}");
            }
        }

        private async Task UpdateStatus(int id)
        {
            int idOrder = id;

            var order = orders.FirstOrDefault(o => o.OrderId == id);

            if( order != null )
            {
                var status = new Order()
                {
                    OrderId = id,
                    UserId = SUser.User.UserId,
                    Status = "Đã hoàn thành",
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                };
                await httpClient.PutAsJsonAsync("api/Order/UpdateStatus/" + idOrder, status);

                var orderu = orderUs.FirstOrDefault(o => o.OrderId == id);

                if (orderu != null)
                {
                    orderu.Status = status.Status;
                    orderu.OrderDate = status.OrderDate;
                    orderu.TotalAmount = status.TotalAmount;
                    orderu.OrderId = status.OrderId;
                    orderu.UserId = status.UserId;
                }
            }

            

            StateHasChanged();
        }

    }
}
