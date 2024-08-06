﻿using System.Net.Http.Json;
using System.Threading.Tasks;
using ASMC6.Shared;
using Microsoft.JSInterop;
using System.Text.Json;
using System;
using System.Diagnostics;

namespace ASMC6.Client.Pages
{
    public partial class Login
    {

        private LoginRequest user = new LoginRequest();
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
                        await JS.InvokeVoidAsync("showLoginAlert", "True");
                        await Task.Delay(500);
                        Navigation.NavigateTo("/",true);

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
    }
}
