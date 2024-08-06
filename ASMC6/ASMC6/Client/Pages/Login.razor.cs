using System.Net.Http.Json;
using System.Threading.Tasks;
using ASMC6.Shared;
using Microsoft.JSInterop;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;

namespace ASMC6.Client.Pages
{
    public partial class Login
    {

        private User user = new User()  ;
        private string Token = "";

        private async Task HandleLogin()
        {
            var response = await httpClient.PostAsJsonAsync("api/AuthJWT/AuthUser", user);
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginRespone>();
                    if (loginResponse != null && loginResponse.SuccsessFull)
                    {
                        Token = loginResponse.Token;

                        var name = user.Email;
                        var expiryTime = DateTime.Now.AddMinutes(30).ToString("o");
                        await _localStorageService.SetItemAsync("authToken", Token);
                        await _localStorageService.SetItemAsync("userName", name);
                        await _localStorageService.SetItemAsync("expiryTime", expiryTime);

                        await Task.Delay(800);

                        await JS.InvokeVoidAsync("showLoginAlert", "True");
                        Navigation.NavigateTo("/", true);
                    }
                    else
                    {
                        await JS.InvokeVoidAsync("showLoginAlert", "False");
                    }
                }
                catch (JsonException ex)
                {
                    Token = $"JSON parse error: {ex.Message}";
                }
            }
            else
            {
                Token = "Server error or invalid request.";
            }
        }

        private void LoginWithGoogle()
        {
            var googleAuthUrl = "https://localhost:5001/api/AuthJWT/signin-google";
            Navigation.NavigateTo(googleAuthUrl, true);
        }
        

        private void LoginWithFacebook()
        {
            var facebookAuthUrl = "https://localhost:5001/api/AuthJWT/signin-facebook";
            Navigation.NavigateTo(facebookAuthUrl, true);
        }
    }
}
