﻿using System.Collections.Generic;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using ASMC6.Shared;
using Microsoft.JSInterop;
using ASMC6.Client.Session;
using System.Net.Http;
using System.Text.Json;

namespace ASMC6.Client.Pages
{
    public partial class Login
    {

        private User user = new User();
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
                        await JS.InvokeVoidAsync("showLoginAlert", "True");
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

        private Task<string> ThucHienDangNhap(string username, string password)
        {

            
            return Task.FromResult("False");
        }
        

    }
}
