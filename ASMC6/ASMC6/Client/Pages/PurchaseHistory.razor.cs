using ASMC6.Client.Session;
using ASMC6.Shared;
using Microsoft.JSInterop;
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
            StartBackgroundTasks();
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
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
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

        private async Task UpdateOrderShipper()
        {
            var lastIndex = orders.Count() - 1;
            
            var order = orders[lastIndex];

            int idOrder = order.OrderId;
            
            if (order != null)
            {
                if(order.Status.Equals("Đang xử lí"))
                {
                    var status = new Order()
                    {
                        OrderId = idOrder,
                        UserId = SUser.User.UserId,
                        Status = "Đang giao hàng",
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                    };
                    await httpClient.PutAsJsonAsync("api/Order/UpdateStatus/" + idOrder, status);

                }
                else if(order.Status.Equals("Đang giao hàng"))
                {
                    var status = new Order()
                    {
                        OrderId = idOrder,
                        UserId = SUser.User.UserId,
                        Status = "Đã nhận hàng",
                        OrderDate = order.OrderDate,
                        TotalAmount = order.TotalAmount,
                    };
                    await httpClient.PutAsJsonAsync("api/Order/UpdateStatus/" + idOrder, status);
                }
                
            }
            StateHasChanged();
        }
        private void StartBackgroundTasks()
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(5000);
                await UpdateOrderShipper();

                await Task.Delay(5000);
                await UpdateOrderShipper();
            });
        }

    }
}
