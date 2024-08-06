using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Components;

namespace ASMC6.Client.Pages
{
    public partial class Restaurant
    {
        [Parameter]
        public int UserId { get; set; }

        private Restaurant restaurant;
        private List<Product> Products = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            await LoadRestaurants();
        }

        private async Task LoadRestaurants()
        {
            restaurant = await httpClient.GetFromJsonAsync<Restaurant>("api/Restaurant/");
        }
    }
}
