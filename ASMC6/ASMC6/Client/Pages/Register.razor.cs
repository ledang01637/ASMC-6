
using ASMC6.Shared;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASMC6.Client.Pages
{
	public partial class Register
	{
        private string errorMessage;
        private string successMessage;
        private string exitsMessage;
        private User user = new User();
        private List<User> listUser;
        protected override async void OnInitialized()
        {
            //await LoadUser();
            user.RoleId = 3;
        }
        private async Task AddUser()
        {
            //var checkEmail = listUser.FirstOrDefault(x => x.Email.Equals(user.Email));
            try
            {
                //if(checkEmail != null)
                //{
                await httpClient.PostAsJsonAsync("api/User/AddUser", user);
                user = new User(); // Reset form
                user.RoleId = 3;
                errorMessage = string.Empty;

                //successMessage = $"Đăng ký thành công";
                //exitsMessage = string.Empty;

                //}
                //else
                //{
                //    errorMessage = string.Empty;
                //    successMessage = string.Empty;
                //}
                //successMessage = string.Empty;
                //errorMessage = string.Empty;
                //exitsMessage = string.Empty;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi: {ex.Message}";
            }
            StateHasChanged();
        }
        //private async Task LoadUser()
        //{
        //    try
        //    {
        //        listUser = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Lỗi khi tải dữ liệu người dùng: {ex.Message}");
        //    }
        //}

    }
}
