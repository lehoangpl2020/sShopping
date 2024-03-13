using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Weather.Application.Commands;
using Weather.Application.Dto;

namespace Weather.Application.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(b => b.BookingTime).Matches("[0-9]{1,2}:[0-9][0-9]");
        }
    }
}
