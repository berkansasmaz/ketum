using System.Collections.Generic;
using Ketum.Entity;
using Ketum.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web
{
    [Route("api/v1/[controller]")]
    public class ApiController : SecureDbController
    {
	
		public IActionResult Success(string message = default(string), object data = default(object), int code = 200){
			return Ok(
				new KTReturn(){
					Success = true,
					Message = message,
					Data = data,
					Code = code
				}
			); //Burada JSON' da dönebilirdim farketmez zaten döneceğim datadan o bunu anlıycak.
		}

		public IActionResult Error(string message = default(string), string internalMessage = default(string), object data = default(object), int code = 400, List<KTReturnError> errorMessage = null){
			var rv = new KTReturn(){
					Success = false,
					//User Message
					Message = message,
					//API User Message
					InternalMessage = internalMessage,
					Data = data,
					Code = code
				};
				
			if(code == 500)
				return StatusCode(500,rv);
			if(code == 401)
				return Unauthorized();
			if(code == 403)
				return Forbid();

			return BadRequest(rv); 
		}
    }
} 