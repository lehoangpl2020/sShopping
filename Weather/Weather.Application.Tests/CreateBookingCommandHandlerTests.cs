using Weather.Application.Commands;
using Weather.Application.Handlers;
using FluentAssertions;
using FluentValidation;
using Weather.Application.Dto;

namespace Weather.Application.Tests
{
    public class CreateBookingCommandHandlerTests
    {
        [Fact]
        public async Task Test1()
        {
            // Given
            var command = new CreateBookingCommand
            {
                BookingTime = "invalid:00",
                Name = "Name"
            };

            // When
            var handler = new CreateBookingCommandHandler();
            // Func<Task> act = async() => await handler.Handle(command, CancellationToken.None);

            Func<Task> act = async() =>  await  handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}