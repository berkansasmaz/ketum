using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web
{
    [Authorize]
    public class SecureController : Controller
    {
    }
}
