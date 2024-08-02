using ASMC6.Client.Session;
using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASMC6.Client.Pages
{
    public partial class Payment
    {
        private List<ASMC6.Shared.Product> cartItems;
        private Order order = new Order();
        private OrderItem orderItem = new OrderItem();
        private decimal Total;
        private string textCode;
        private string voucher = "";
        private decimal Fee = 25000;
        private bool isExpressChecked;
        private bool isRuleChecked;

        protected override async Task OnInitializedAsync()
        {
            cartItems = await CartService.GetCartAsync();
            CalculateTotal();
            await GenerateQrCode();
        }
        private void CalculateTotal()
        {
            Total = 0;
            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    Total += item.Price;
                }
                if (!string.IsNullOrEmpty(textCode) && textCode.Equals(voucher))
                {
                    Total -= 15000;
                }
            }
            Total += Fee;

        }

        private void ApplyVoucher(ChangeEventArgs e)
        {
            voucher = e.Value.ToString();
            CalculateTotal();
        }

        private async Task GenerateQrCode()
        {
            Random rd = new Random();
            textCode = rd.Next(100000, 999999).ToString();
            
            await JSRuntime.InvokeVoidAsync("clearQrCode");
            await JSRuntime.InvokeVoidAsync("generateQrCode", textCode);
        }
        private void UpdateFee(ChangeEventArgs e)
        {
            isExpressChecked = (bool)e.Value;
            Fee = isExpressChecked ? 25000 : 0;
            CalculateTotal();
        }
        private void Pay(ChangeEventArgs e)
        {
            isRuleChecked = (bool)e.Value;
        }
        private async Task<string> PayMoney()
        {
            order = new Order()
            {
                UserId = SUser.User.UserId,
                OrderDate = DateTime.Now,
                TotalAmount = Total,
                Status = "Đang xử lí"
            };

            await SaveOrder(order);

            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    orderItem = new OrderItem()
                    {
                        ProductId = item.ProductId,
                        OrderId = order.OrderId,
                        Quantity = 1,
                        Price = item.Price
                    };
                    await SaveOrderItem(orderItem);
                }
            }
            
            return ("AddOrder");
        }
        private async Task History()
        {

            var addSuccess  = await PayMoney();
            if(addSuccess == "AddOrder")
            {
                await JSRuntime.InvokeVoidAsync("showAddOrder", addSuccess);
                Navigation.NavigateTo("/history");
            }
            
        }
        private async Task SaveOrder(Order _order)
        {
            var response = await httpClient.PostAsJsonAsync("api/Order/AddOrder", _order);

            if (response.IsSuccessStatusCode)
            {
                var createdOrder = await response.Content.ReadFromJsonAsync<Order>();
                _order.OrderId = createdOrder.OrderId;
            }
            StateHasChanged();
        }
        private async Task SaveOrderItem(OrderItem _orderItem)
        {
            await httpClient.PostAsJsonAsync("api/OrderItem/AddOrderItem", _orderItem);
        }
    }
}
