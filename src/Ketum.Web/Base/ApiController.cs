using System;
using System.Collections.Generic;
using Ketum.Entity;
using Ketum.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web
{
    [Route("api/v1/[controller]")]
    public class ApiController : SecureDbController
    {
        private UserManager<KTUser> _userManager;

        public UserManager<KTUser> UserManager => _userManager ??
                                                  (UserManager<KTUser>) HttpContext?.RequestServices.GetService(
                                                      typeof(UserManager<KTUser>));

        public Guid UserId
        {
            get
            {
                var userId = UserManager.GetUserId(User);
                return Guid.Parse(userId);
            }
        }

        [NonAction] // Bu non action belirtmezsek eğer asp.net bunu action olarak algılar ve .../Success miş gibi davranmasını sağlar.
        public IActionResult Success(string message = default, object data = default, int code = 200)
        {
            return Json(
                new KTReturn
                {
                    Success = true,
                    Message = message,
                    Data = data,
                    Code = code
                }
            ); //Burada JSON' da dönebilirdim farketmez zaten döneceğim datadan o bunu anlıycak.
        }

        [NonAction]
        public IActionResult Error(string message = default, string internalMessage = default, object data = default,
            int code = 400, List<KTReturnError> errorMessage = null)
        {
            var rv = new KTReturn
            {
                Success = false,
                //User Message
                Message = message,
                //API User Message
                InternalMessage = internalMessage,
                Data = data,
                Code = code
            };

            if (rv.Code == 500)
                return StatusCode(500, rv);
            if (rv.Code == 401)
                return Unauthorized();
            if (rv.Code == 403)
                return Forbid();
            if (rv.Code == 404)
                return NotFound(rv);

            return BadRequest(rv);
        }
    }
}
