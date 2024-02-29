using GameOfChanceCqrs.Api.Filters;
using GameOfChanceCqrs.Application.Bets.PlaceBet;
using GameOfChanceCqrs.Application.Bets.ViewModels.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameOfChanceCqrs.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    public class BetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<BetOutputVm>> PlaceBet([FromBody] PlaceBetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
