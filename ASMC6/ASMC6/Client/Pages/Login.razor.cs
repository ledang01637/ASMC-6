﻿using System.Collections.Generic;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;
using ASMC6.Shared;
using Microsoft.JSInterop;
using ASMC6.Client.Session;

namespace ASMC6.Client.Pages
{
    public partial class Login
    {
        private string Username;
        private string Password;
        private List<User> listUser;
        private List<Role> listRole;


        protected override async Task OnInitializedAsync()
        {
            await LoadUser();
            await LoadRole();
        }

        private async Task DangNhap()
        {

            var loginSuccess = await ThucHienDangNhap(Username, Password);

            await JS.InvokeVoidAsync("showLoginAlert", loginSuccess);


        }

        private Task<string> ThucHienDangNhap(string username, string password)
        {

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return Task.FromResult("InputNull");
            }

            var isSuccsess = listUser.FirstOrDefault(u => u.Email.Equals(Username) && u.Password.Equals(Password));

            if (isSuccsess != null)
            {
                var role = listRole.FirstOrDefault(r => r.RoleId.Equals(isSuccsess.RoleId));

                if (isSuccsess.IsDelete == true)
                {
                    return Task.FromResult("Block");
                }
                SUser.User = isSuccsess;
                if (role != null)
                {
                    if (role.Name.ToLower().Equals("admin"))
                    {
                        Navigation.NavigateTo("/");
                    }
                    else if (role.Name.ToLower().Equals("restaurant"))
                    {
                        Navigation.NavigateTo("/");
                    }
                    else if (role.Name.ToLower().Equals("user"))
                    {
                        Navigation.NavigateTo("/");
                    }
                }
                return Task.FromResult("True");

            }
            return Task.FromResult("False");
        }
        private async Task LoadUser()
        {
            try
            {
                listUser = await httpClient.GetFromJsonAsync<List<User>>("api/User/GetUsers");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu người dùng: {ex.Message}");
            }
        }
        private async Task LoadRole()
        {
            try
            {
                listRole = await httpClient.GetFromJsonAsync<List<Role>>("api/Role/GetRoles");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tải dữ liệu Role: {ex.Message}");
            }
        }

    }
}
