﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Linq;

namespace ASMC6.Client.Pages
{
    public partial class Cart
    {
        private List<ASMC6.Shared.Product> cartItems;
        private decimal Total;
        private decimal Fee = 25000;
        private decimal Pay;

        protected override async Task OnInitializedAsync()
        {
            var email = await _localStorageService.GetItemAsync("userName");

            if (email is null)
            {
                Navigation.NavigateTo("/login");
            }

            cartItems = await CartService.GetCartAsync();
            CalculateTotal();
        }

        private async Task RemoveFromCart(ASMC6.Shared.Product product)
        {
            if(product != null)
            {
                await CartService.RemoveItemFromCartAsync(product);
                cartItems = await CartService.GetCartAsync();
                CalculateTotal();
            }
        }
        private async Task ThanhtoanAsync()
        {
            if(cartItems.Count() > 0)
            {
                Navigation.NavigateTo("/payment");
            }
            else
            {
                await JS.InvokeVoidAsync("showLoginAlert", "EmptyPro");
            }
        }
        private void CalculateTotal()
        {
            Total = 0;
            foreach (var item in cartItems)
            {
                Total += item.Price;
            }
            Pay = Total + Fee;
        }
    }
}
