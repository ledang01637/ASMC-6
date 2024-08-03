using ASMC6.Client.Session;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace ASMC6.Client.Shared
{
    public partial class MainLayout
    {
        private bool IsLoggedIn;
        private string UserName;

        //protected override async Task OnInitializedAsync()
        //{
        //    // Đăng ký sự kiện StateChanged để cập nhật giao diện khi trạng thái thay đổi
        //    AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;

        //    await CheckAuthenticationState();
        //}

        //private async Task CheckAuthenticationState()
        //{
        //    var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        //    IsLoggedIn = authState.User.Identity.IsAuthenticated;

        //    if (IsLoggedIn)
        //    {
        //        UserName = authState.User.Identity.Name;
        //    }
        //    else
        //    {
        //        UserName = string.Empty;
        //    }

        //    StateHasChanged();
        //}

        //private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        //{
        //    await CheckAuthenticationState();
        //}

        //public void Dispose()
        //{
        //    // Hủy đăng ký sự kiện khi component bị hủy
        //    AuthenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;
        //}
    }
}
