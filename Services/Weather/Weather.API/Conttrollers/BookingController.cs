using Microsoft.AspNetCore.Mvc;
using Weather.Application.Commands;

namespace Weather.API.Conttrollers
{

    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private MediatR.IMediator _mediator;

        public BookingController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBooking(CreateBookingCommand command)
        {
            var booking = await _mediator.Send(command);
            return Ok(booking);
        }
    }
}
