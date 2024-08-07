using ASMC6.Client.Session;
using ASMC6.Server.Service;
using ASMC6.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
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
        private decimal TotalAmount;
        private string textCode;
        private string voucher = "";
        private decimal Fee = 25000;
        private decimal expressDelivery = 25000;
        private bool isExpressChecked;
        private bool isRuleChecked;
        private int idUser;

        private List<User> listUser;
        private User user = new User();
        protected override async Task OnInitializedAsync()
        {
            var email = await _localStorageService.GetItemAsync("userName");

            if (email is null)
            {
                Navigation.NavigateTo("/login");
            }

            listUser = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");

            if (listUser != null)
            {
                user = listUser.FirstOrDefault(u => u.Email.Equals(email));
            }
            cartItems = await CartService.GetCartAsync();
            CalculateTotal();
            await GenerateQrCode();
        }
        private void CalculateTotal()
        {
            Total = 0;
            TotalAmount = 0;
            if (cartItems != null)
            {
                foreach (var item in cartItems)
                {
                    Total += item.Price;
                    TotalAmount += item.Price;
                }
                if (!string.IsNullOrEmpty(textCode) && textCode.Equals(voucher))
                {
                    Total -= 15000;
                }
            }
            Total += (Fee + expressDelivery);

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
            expressDelivery = isExpressChecked ? 25000 : 0;
            CalculateTotal();
        }
        private void Pay(ChangeEventArgs e)
        {
            isRuleChecked = (bool)e.Value;
        }
        private async Task<string> PayMoney()
        {
            await ReadToken();

            order = new Order()
            {
                UserId = idUser,
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

        private async Task ReadToken()
        {
            var token = await _localStorageService.GetItemAsync("authToken");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            idUser = int.Parse(userId);
        }
        private async Task History()
        {

            var addSuccess  = await PayMoney();
            if(addSuccess == "AddOrder")
            {
                await JSRuntime.InvokeVoidAsync("showAlert", addSuccess);
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
