using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Application.Dto;

namespace Weather.Application.Commands
{
    public class CreateBookingCommand : IRequest<BookingDto>
    {
        public string BookingTime { get; set; }
        public string Name { get; set; }

    }
}
