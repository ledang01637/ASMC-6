
using ASMC6.Shared;
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
        private List<User> checkuser = new List<User>();
        protected override void OnInitialized()
        {
            user.RoleId = 3; // Thiết lập RoleId bằng 3
        }
        private async Task AddUser()
        {
            //var exits = checkuser.FirstOrDefault(x => x.Email.Equals(user.Email));

            try
            {
                await httpClient.PostAsJsonAsync("api/User/AddUser", user);
                user = new User(); // Reset form
                user.RoleId = 3; // Đảm bảo RoleId luôn bằng 3
                errorMessage = string.Empty; // Xóa thông báo lỗi (nếu có)
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi: {ex.Message}";
            }

            StateHasChanged();
        }

    }
}
