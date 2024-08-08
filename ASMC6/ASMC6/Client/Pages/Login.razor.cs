using System.Net.Http.Json;
using System.Threading.Tasks;
using ASMC6.Shared;
using Microsoft.JSInterop;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Policy;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Collections.Generic;
using ASMC6.Client.Session;

namespace ASMC6.Client.Pages
{
    public partial class Login
    {

        private User user = new User();
        private List<User> users = new List<User>();
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
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadToken(Token) as JwtSecurityToken;
                        var emailUser = jsonToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

                        await GetUsers(emailUser);

                        if(user.IsDelete)
                        {
                            await JS.InvokeVoidAsync("showAlert", "Block");
                            return;
                        }
                        var name = user.Email;
                        var expiryTime = DateTime.Now.AddMinutes(30).ToString("o");
                        await _localStorageService.SetItemAsync("authToken", Token);
                        await _localStorageService.SetItemAsync("userName", name);
                        await _localStorageService.SetItemAsync("expiryTime", expiryTime);
                        await _localStorageService.SetItemAsync("userRoleId", user.RoleId.ToString());
                        await JS.InvokeVoidAsync("showAlert", "True");
                        await Task.Delay(1000);
                        Navigation.NavigateTo("/", true);
                    }
                    else
                    {
                        await JS.InvokeVoidAsync("showAlert", "False");
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

        private async Task GetUsers(string email)
        {
            try
            {
                users = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");

                if (users!= null)
                {
                    user = users.FirstOrDefault(a => a.Email.Equals(email));
                    SUser.User = user;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erorr:: " + ex.Message);
            }
        }
    }
}
