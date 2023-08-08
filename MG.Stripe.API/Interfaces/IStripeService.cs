using StripPaymentPOC.Models;

namespace StripPaymentPOC.Interfaces
{
	public interface IStripeService
	{
		Task<string> CheckOut(ProductModel product);
	}
}
