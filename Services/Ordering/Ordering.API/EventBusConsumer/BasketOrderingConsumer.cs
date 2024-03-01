using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer;

//IConsumer Implementation 
public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketOrderingConsumer> _logger;

    public BasketOrderingConsumer(IMediator mediator, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope =  _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}",
            context.Message.CorrelationId);
        //var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
        var command = new CheckoutOrderCommand()
        {
            UserName = context.Message.UserName,
            AddressLine = context.Message.AddressLine,
            CardName = context.Message.CardName,
            CardNumber = context.Message.CardNumber,
            Country = context.Message.Country,
            CVV = context.Message.Cvv,
            EmailAddress = context.Message.EmailAddress,
            Expiration = context.Message.Expiration,
            FirstName = context.Message.FirstName,
            LastName = context.Message.LastName,
            PaymentMethod = context.Message.PaymentMethod,
            State = context.Message.State,
            TotalPrice = context.Message.TotalPrice,
            ZipCode = context.Message.ZipCode

        };
        var result = await _mediator.Send(command);
        _logger.LogInformation($"Basket checkout event completed!!!");
    }
}