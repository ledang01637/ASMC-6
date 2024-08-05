using ASMC6.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASMC6.Client.Pages
{
    public partial class DemoLoginAdmin
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
                    }
                    else
                    {
                        Token = loginResponse?.Error ?? "Login failed.";
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
