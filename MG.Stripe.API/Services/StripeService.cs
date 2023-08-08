using Stripe;
using Stripe.Checkout;
using StripPaymentPOC.Interfaces;
using StripPaymentPOC.Models;

namespace StripPaymentPOC.Services
{
	public class StripeService : IStripeService
	{
		private readonly ILogger<StripeService> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public StripeService(ILogger<StripeService> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<string> CheckOut(ProductModel product)
		{
			try
			{
				// Get the base URL
				var request = _httpContextAccessor.HttpContext!.Request;
				var baseUrl = $"{request.Scheme}://{request.Host}";

				var options = new SessionCreateOptions
				{
					// Stripe calls these user defined endpoints
					//SuccessUrl = $"http://localhost:4200/success",
					//CancelUrl = $"http://localhost:4200/canceled",
					// Stripe calls these user defined endpoints
					SuccessUrl = $"{baseUrl}/payment/success?sessionId=" + "{CHECKOUT_SESSION_ID}",
					CancelUrl = baseUrl + "/payment/canceled",
					PaymentMethodTypes = new List<string>
					{
						"card"
					},
					LineItems = new List<SessionLineItemOptions>
					{
						new()
						{
							 Price= product.ProductApiId,
						   Quantity = product.Quantity,
						},
					},
					Mode = "payment", // One time payment
					InvoiceCreation = new SessionInvoiceCreationOptions { Enabled = true }
				};

				var service = new SessionService();
				var session = await service.CreateAsync(options);

				return session.Id;
			}
			catch (StripeException ex)
			{
				_logger.LogError("error into Stripe Service on CheckOut() " + ex.StripeError.Message);
				throw;
			}
			catch (Exception e)
			{
				_logger.LogError("error into Stripe Service on CheckOut() " + e.Message);
				throw;


			}
		}
	}
}
