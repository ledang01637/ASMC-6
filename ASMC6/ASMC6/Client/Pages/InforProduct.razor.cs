using System.Collections.Generic;
using System.Net.Http.Json;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class InforProduct
    {
        [Parameter]
        public int id { get; set; }

        private ASMC6.Shared.Product prod;

        protected override async Task OnParametersSetAsync()
        {
            await GetInforProduct(id);
        }

        private async Task GetInforProduct(int id)
        {
            try
            {
                prod = await httpClient.GetFromJsonAsync<ASMC6.Shared.Product>("api/Product/GetInforProduct/" + id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
