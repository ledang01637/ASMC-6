
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
        private User user = new User();
        private List<User> listUser = new List<User>();
        protected override async Task OnInitializedAsync()
        {
            await LoadUser();
            user.RoleId = 3;
        }
        private async Task AddUser()
        {
            try
            {
                var exitsEmail = listUser.FirstOrDefault(x => x.Email.Equals(user.Email));
                var exitsPhone = listUser.FirstOrDefault(x => x.Phone.Equals(user.Phone));

                if (exitsEmail != null)
                {
                    await JS.InvokeVoidAsync("showRegisterAlert", "InputEmailExits");
                }
                else if (exitsPhone != null)
                {
                    await JS.InvokeVoidAsync("showRegisterAlert", "InputPhoneExits");
                }
                else
                {
                    var success =  await httpClient.PostAsJsonAsync("api/User/AddUser", user);
                    if (success.IsSuccessStatusCode)
                    {
                        user = new User();
                        errorMessage = string.Empty;
                        await JS.InvokeVoidAsync("showRegisterAlert", "success");
                    }
                    else
                    {
                        await JS.InvokeVoidAsync("showRegisterAlert", "Error");
                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi: {ex.Message}";
            }

            StateHasChanged();
        }
        private async Task LoadUser()
        {
            try
            {
                listUser = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }
}
