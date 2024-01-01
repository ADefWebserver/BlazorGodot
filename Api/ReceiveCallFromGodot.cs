using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BlazorGodot.Api
{
    [ApiController]
    [Route("[controller]")]
    public class ReceiveCallFromGodot : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserData userData)
        {
            // Handle the received data (e.g., authentication, logging, etc.)

            MessageData objMessageData = new MessageData();
            objMessageData.message = $"Data received successfully! UserName = {userData.userName} - Token = {userData.hTTPToken}";

            // Return objMessageData as JSON
            return Ok(objMessageData);  
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Return a response
            return Ok(new { Message = "Data received successfully." });
        }
    }

    public class UserData
    {
        public string? userName { get; set; }
        public string? hTTPToken { get; set; }
    }

    public class MessageData
    {
        public string? message { get; set; }
    }
}
