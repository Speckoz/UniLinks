using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using UniLinks.Client.Site.Services;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Pages.Coordinator
{
    public partial class AuthPage
    {
        private string email;
        private string password;
        private string show = "collapse";

        [Inject]
        private AccountService AccountService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        private async Task AuthAccountTaskAsync()
        {
            if (await AccountService.AuthAccountTaskAsync(new LoginRequestModel { Email = email, Password = password }))
            {
                Navigation.NavigateTo("/Coordinator");
            }
            else
                show = nameof(show);
        }

        private void HideAlert() => show = "collapse";
    }
}