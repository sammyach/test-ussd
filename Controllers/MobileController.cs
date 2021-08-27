using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using test_ussd.Models;

namespace test_ussd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
		[Route("ussd")]
		// specify the actual route, your url will look like... localhost:8080/api/mobile/ussd...
		[HttpPost, ActionName("ussd")]
		// state that the method you intend to create is a POST 
		public HttpResponseMessage ussd([FromBody] ServerResponse ServerResponse)
		{
			// declare a complex type as input parameter
			HttpResponseMessage rs;
			string response;
			if (ServerResponse.text == null)
			{
				ServerResponse.text = "";
			}

			// loop through the server's text value to determine the next cause of action
			if (ServerResponse.text.Equals("", StringComparison.Ordinal))
			{
				// always include a 'CON' in your first statements
				response = "CON This is AfricasTalking \n";

								response += "1. Get your phone number";
			}
			else if (ServerResponse.text.Equals("1", StringComparison.Ordinal))
			{
				response = "END Your phone number is " + ServerResponse.phoneNumber;

				//the last response starts with an 'END' so that the server understands that it's the final response
			}
			else
			{
				response = "END invalid option";
			}
			rs = new HttpResponseMessage() { StatusCode = HttpStatusCode.Created, Content = new StringContent(JsonConvert.SerializeObject(response))};//Request. CreateResponse(HttpStatusCode.Created, response);

			// append your response to the HttpResponseMessage and set content type to text/plain, exactly what the server expects
			rs.Content = new StringContent(response, Encoding.UTF8, "text/plain");
			// finally return your response to the server
			return rs;

		}
	}


}
