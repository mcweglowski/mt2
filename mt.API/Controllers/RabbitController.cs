using MassTransit;
using Microsoft.AspNetCore.Mvc;
using mt.API.Requests;
using mt.Contracts.ExchangeDirectContracts;
using mt.Contracts.RemoteProcedureCallContracts;

namespace mt.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitController : ControllerBase
{
    private readonly IBus _bus;
    private readonly ILogger _logger;
	private readonly IRequestClient<IRemoteProcedureCallContract> _requestClient;

	public RabbitController(IBus bus,
		ILogger<RabbitController> logger,
        IRequestClient<IRemoteProcedureCallContract> requestClient)
	{
		_bus = bus;
		_logger = logger;
        _requestClient = requestClient;
	}

	[HttpPost("direct/")]
	public async Task Direct([FromBody] DataRequest request)
	{
		_logger.LogInformation($"{request.Id} {request.Name} {request.Amount}");

		await _bus.Publish<ExchangeDirectContract>(new
		{
			request.Id,
			NodeId = "500",
			request.Name,
			request.Amount
		});
	}

	[HttpPost("remoteProcedureCall/")]
	public async Task<IActionResult> RemoteProcedureCall([FromBody] DataRequest request)
	{
        _logger.LogInformation($"{request.Id} {request.Name} {request.Amount}");
		
		var response = await _requestClient.GetResponse<IRemoteProcedureCallResponse>(
			new { request.Id, request.Name, request.Amount});

		return Ok(response);
    }
}
