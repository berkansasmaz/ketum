using Microsoft.AspNetCore.Authorization;

namespace Ketum.Web
{
    [Authorize]
    public class SecureDbController : DbController
    {
    }
}
