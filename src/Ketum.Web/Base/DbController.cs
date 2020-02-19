using Ketum.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web
{
    public class DbController : Controller
    {
        private KTDBContext _db;
        public KTDBContext Db => _db ?? (KTDBContext) HttpContext?.RequestServices.GetService(typeof(KTDBContext));
    }
}
