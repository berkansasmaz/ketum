using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Ketum.Web.Pages
{
    public class IndexModel : KetumPageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}