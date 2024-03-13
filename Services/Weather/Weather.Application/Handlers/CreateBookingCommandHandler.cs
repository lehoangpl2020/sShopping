using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Application.Commands;
using Weather.Application.Dto;
using FluentValidation;
using Weather.Application.Validators;
using Weather.Core.Entities;
using Microsoft.Extensions.Options;

namespace Weather.Application.Handlers
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private const int MaxSimultaneousBookings = 4;
        private static List<Booking> bookings = new List<Booking>();
        private static readonly TimeSpan BusinessStartTime = new TimeSpan(9, 0, 0);
        private static readonly TimeSpan BusinessEndTime = new TimeSpan(17, 0, 0);

        public CreateBookingCommandHandler(IOptions<BookingSettings> bookingSettings)
        {

        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            //var createBookingCommandValidator = new CreateBookingCommandValidator();
            //var validaionResult = createBookingCommandValidator.Validate(request);
            //if (!validaionResult.IsValid)
            //{
            //    throw new ValidationException(validaionResult.Errors);
            //}

            //await Task.FromResult(2);

            // Check if the booking time is within business hours
            if (!TimeSpan.TryParse(request.BookingTime, out TimeSpan bookingTime) || !IsBusinessHour(bookingTime))
            {
                // return BadRequest("Booking time is not valid or outside business hours.");
                throw new ValidationException("Booking time is not valid or outside business hours.");
            }


            throw new NotImplementedException();

            return new BookingDto { BookingId = Guid.NewGuid() };
        }

        private bool IsBusinessHour(TimeSpan time)
        {
            return time >= BusinessStartTime && time <= BusinessEndTime;
        }
    }
}

