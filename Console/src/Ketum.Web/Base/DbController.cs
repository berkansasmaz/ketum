using Microsoft.AspNetCore.Mvc;
using Ketum.Entity;

namespace Ketum.Web
{
    public class DbController : Controller
    {
         private KTDBContext _db;
        public KTDBContext Db => _db ?? (KTDBContext)HttpContext?.RequestServices.GetService(typeof(KTDBContext));
    }
}