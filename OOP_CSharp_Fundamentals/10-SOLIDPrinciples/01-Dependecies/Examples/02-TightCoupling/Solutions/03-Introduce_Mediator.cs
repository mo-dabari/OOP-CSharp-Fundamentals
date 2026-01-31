// ====================================
// ✅ الحل 3: Introduce Mediator
// ====================================
namespace GoodExample_IntroduceMediator
{
    // بدل ما كل Object يكلم التاني مباشرة
    // كلهم يكلموا Mediator واحد

    // الـ Mediator
    public interface IMediator
    {
        void Send<TRequest>(TRequest request);
    }

    public class Mediator : IMediator
    {
        private readonly Dictionary<Type, object> _handlers = new();

        public void RegisterHandler<TRequest, THandler>(THandler handler)
            where THandler : IHandler<TRequest>
        {
            _handlers[typeof(TRequest)] = handler;
        }

        public void Send<TRequest>(TRequest request)
        {
            if (_handlers.TryGetValue(typeof(TRequest), out var handler))
            {
                ((IHandler<TRequest>)handler).Handle(request);
            }
        }
    }

    public interface IHandler<T>
    {
        void Handle(T request);
    }

    // Requests
    public class CreateOrderRequest
    {
        public Order Order { get; set; }
    }

    public class SendEmailRequest
    {
        public string Email { get; set; }
        public string Message { get; set; }
    }

    // Handlers
    public class CreateOrderHandler : IHandler<CreateOrderRequest>
    {
        private readonly IMediator _mediator;

        public CreateOrderHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Handle(CreateOrderRequest request)
        {
            // حفظ الطلب
            Console.WriteLine("Order Created");

            // إرسال إشعار بدون معرفة تفاصيل EmailService
            _mediator.Send(new SendEmailRequest
            {
                Email = "customer@email.com",
                Message = "Order created successfully"
            });
        }
    }

    public class SendEmailHandler : IHandler<SendEmailRequest>
    {
        public void Handle(SendEmailRequest request)
        {
            Console.WriteLine($"Sending email to {request.Email}: {request.Message}");
        }
    }

    // ✅ الاستخدام:
    // var mediator = new Mediator();
    // mediator.RegisterHandler<CreateOrderRequest, CreateOrderHandler>(new CreateOrderHandler(mediator));
    // mediator.RegisterHandler<SendEmailRequest, SendEmailHandler>(new SendEmailHandler());
    // mediator.Send(new CreateOrderRequest { Order = new Order() });
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

