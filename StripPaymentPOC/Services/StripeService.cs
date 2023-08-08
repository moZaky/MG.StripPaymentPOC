using StripPaymentPOC.Interfaces;

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
	}
}
