using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;

        const string secret = "whsec_2da7bfda0897cc422d689ea36ca2182fa0b88ccef8b1d64a4d4097188795b4af";
        //const string secret = "whsec_2da7bfda0897cc422d689ea36ca2182fa0b88ccef8b1d64a4d4097188795b4af";
        public PaymentService(ICartService cartService,IAuthService authService,IOrderService orderService)
        {
            StripeConfiguration.ApiKey = "sk_test_51LATD9SJyYa0GM1fOVh4kcaTexSa5AEVruZRymrGc9ZfLxPtOTupOdM1sT78gHqCY9q0Sq6Fs5o5ulnPowvJqJGL00x6Lwi1vD";
            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();

            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "inr",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title,
                        Images = new List<string>
                        {
                            product.ImageUrl,
                        }
                    }

                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                //ShippingAddressCollection = 
                //    new SessionShippingAddressCollectionOptions
                //    {
                //        AllowedCountries = new List<string> { "IN" }
                //    },
               
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7125/order-success",
                CancelUrl = "https://localhost:7125/cart"
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session;


        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        secret

                    );
                if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }
                return new ServiceResponse<bool> { Data = true };

            }catch(StripeException e)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }
    }
}
