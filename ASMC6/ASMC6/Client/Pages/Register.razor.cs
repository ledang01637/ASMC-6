
using ASMC6.Shared;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
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
                    await JS.InvokeVoidAsync("showAlert", "InputEmailExits");
                }
                else if (exitsPhone != null)
                {
                    await JS.InvokeVoidAsync("showAlert", "InputPhoneExits");
                }
                else
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    user.Password = hashedPassword;

                    var success =  await httpClient.PostAsJsonAsync("api/User/AddUser", user);
                    if (success.IsSuccessStatusCode)
                    {
                        user = new User();
                        errorMessage = string.Empty;
                        await JS.InvokeVoidAsync("showAlert", "success");
                        await Task.Delay(1000);
                        Navigation.NavigateTo("/login",true);
                    }
                    else
                    {
                        await JS.InvokeVoidAsync("showAlert", "Error");
                    }

                }
            }
            catch (Exception ex)
            {
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");
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
                var query = $"[C#] fix error: {ex.Message}";
                await JS.InvokeVoidAsync("openChatGPT", query);
                Console.WriteLine($"Error hiding user: {ex.Message}");
            }
        }
    }
}
