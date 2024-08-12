using ASMC6.Server.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Net.Http.Json;
using ASMC6.Shared;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace ASMC6.Client.Pages
{
    public partial class AdminManageCategory
    {
        private List<ASMC6.Shared.Category> listCategory = new List<ASMC6.Shared.Category>();
        private List<ASMC6.Shared.Category> filteredCategory = new List<ASMC6.Shared.Category>();
        private bool isLoaded = false;
        private string errorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadCategories();
            isLoaded = true;
        }

        private async Task LoadCategories()
        {
            try
            {
                listCategory = await httpClient.GetFromJsonAsync<List<ASMC6.Shared.Category>>("api/Category/GetCategory");
                filteredCategory = listCategory;
            }
            catch (Exception ex)
            {
                errorMessage = $"Error loading categories: {ex.Message}";
            }
        }

        private async Task DeleteCategory(int categoryId)
        {
            try
            {
                var category = listCategory.FirstOrDefault(p => p.CategoryId == categoryId);
                if (category != null)
                {
                    await httpClient.DeleteAsync($"api/Category/{categoryId}");
                    await LoadCategories();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error deleting category: {ex.Message}";
            }
        }

        private void EditCategory(int categoryId)
        {
            Navigation.NavigateTo($"/editcategory/{categoryId}");
        }

        private void FilterCategories(ChangeEventArgs e)
        {
            var searchTerm = e.Value.ToString().ToLower();
            filteredCategory = string.IsNullOrWhiteSpace(searchTerm)
                ? listCategory
                : listCategory.Where(p => p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm)).ToList();
        }

    }
}
