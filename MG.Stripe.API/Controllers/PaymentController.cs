using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe.Checkout;
using StripPaymentPOC.Interfaces;
using StripPaymentPOC.Models;

namespace StripPaymentPOC.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PaymentController : ControllerBase
	{
		private readonly IStripeService _stripeService;
		private readonly ILogger<PaymentController> _logger;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _frontendSuccessUrl;
		private readonly string _frontendCanceledUrl;

		public PaymentController(IStripeService stripeService,
			ILogger<PaymentController> logger,
			 IHttpContextAccessor httpContextAccessor)
		{
			this._stripeService = stripeService;
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
			var request = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = $"{request.Scheme}://{request.Host}";
			_frontendSuccessUrl = baseUrl + "/success";
			_frontendCanceledUrl = baseUrl + "/canceled";
		}
		[HttpGet("demo")]
		public IActionResult Get()
		{

			return Ok("works");
		}
		[HttpPost("PlaceOrder")]
		public async Task<IActionResult> PlaceOrder([FromBody] ProductModel order)
		{

			try
			{
				var sessionId = await _stripeService.CheckOut(order);

				return Ok(JsonConvert.SerializeObject(sessionId));

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		/// <summary>
		/// this API is going to be hit when order is a failure
		/// </summary>
		/// <returns>A redirect to the front end success page</returns>
		[HttpGet("canceled")]
		public async Task<IActionResult> CheckoutCanceled([FromQuery] string sessionId)
		{
			try
			{

				// Insert here failure data in data base
				return Redirect(_frontendCanceledUrl);
			}
			catch (Exception ex)
			{
				_logger.LogError("error into order Controller on route /canceled " + ex.Message);
				return BadRequest();
			}


		}
		[HttpGet("success")]
		public async Task<IActionResult> CheckoutSuccess([FromQuery] string sessionId)
		{
			try
			{
				var sessionService = new SessionService();
				var session = sessionService.Get(sessionId);




				return Redirect(_frontendSuccessUrl);
			}
			catch (Exception ex)
			{
				_logger.LogError("error into order Controller on route /success " + ex.Message);
				return BadRequest();
			}


		}

	}
}
