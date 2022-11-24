using MassTransit;
using Microsoft.AspNetCore.Mvc;
using mt.API.Requests;
using mt.Contracts.Contracts;

namespace mt.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RabbitController : ControllerBase
{
	private readonly ILogger _logger;
	private readonly IRequestClient<IRemoteProcedureCallContract> _requestClient;

	public RabbitController(ILogger<RabbitController> logger,
        IRequestClient<IRemoteProcedureCallContract> requestClient)
	{
		_logger = logger;
        _requestClient = requestClient;
	}

	[HttpPost("direct/")]
	public void Direct([FromBody] DataRequest request)
	{
		_logger.LogInformation($"{request.Id} {request.Name} {request.Amount}");
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
