using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace ASMC6.Client.Pages
{
    public partial class Cart
    {
        private List<ASMC6.Shared.Product> cartItems;
        private decimal Total;
        protected override async Task OnInitializedAsync()
        {
            cartItems = await CartService.GetCartAsync();
        }
    }
}
