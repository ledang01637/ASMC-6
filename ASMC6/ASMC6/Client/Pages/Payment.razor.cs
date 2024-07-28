using ASMC6.Server.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace ASMC6.Client.Pages
{
    public partial class Payment
    {
        private List<ASMC6.Shared.Product> cartItems;
        private decimal Total;
        private string textCode;
        private string voucher = "";
        private decimal Fee = 25000;

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

    }
}
