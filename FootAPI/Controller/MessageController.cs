using FootAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

namespace FootAPI.Controller
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/message")]
    [Produces("application/json")]
    public class MessageController : ControllerBase
    {
        [HttpPost("startmeasure")]
        public IActionResult StartMeasure(
            [FromBody] StartMeasureModel measureModel,
            [FromServices] RabbitMQConfigurations configurations
        )
        {
            var factory = new ConnectionFactory()
            {
                HostName = configurations.HostName,
                Port = configurations.Port,
                UserName = configurations.UserName,
                Password = configurations.Password,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Measure",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string Message = $"Mac={measureModel.MacAddress};UserId={measureModel.UserId}";
                var body = Encoding.UTF8.GetBytes(Message);

                channel.BasicPublish(exchange: "", "Measure", null, body);
            }

            return Ok(true);
        }
    }
}