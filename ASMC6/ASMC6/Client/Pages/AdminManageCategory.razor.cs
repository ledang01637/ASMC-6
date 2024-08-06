using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageCategory
    {
        private List<ASMC6.Shared.Category> listCate;
        private ASMC6.Shared.Category cate = new ASMC6.Shared.Category();

        protected override async Task OnInitializedAsync()
        {
            await LoadCategory();
        }

        private async Task LoadCategory()
        {
            try
            {
                listCate = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Category>>("api/Category/GetCategory");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void EditRest(int CategoryId)
        {
            Navigation.NavigateTo($"/editcategory/{CategoryId}");
        }

        private void DeleteRest(int CategoryId)
        {
            Navigation.NavigateTo($"/deletecategory/{CategoryId}");
        }


        //chuyển trang
        //protected override void OnInitialized()
        //{
        //    // Automatically redirect after a short delay
        //    Task.Delay(2000).ContinueWith(_ => Navigation.NavigateTo("/", true));
        //}

    }
}
