using MassTransit;
using Microsoft.AspNetCore.Mvc;
using mt.API.Requests;

namespace mt.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitController : ControllerBase
{
	private readonly ILogger _logger;
	private readonly IBus _bus;

	public RabbitController(ILogger<RabbitController> logger,
		IBus bus)
	{
		_logger = logger;
		_bus = bus;
	}

	[HttpPost("direct/")]
	public void Direct([FromBody] DataRequest request)
	{
		_logger.LogInformation($"{request.Id} {request.Name} {request.Amount}");
	}
}
