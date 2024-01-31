using Braintree;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<SubscriptionResponse> PayAsync(PaymentRequest requestModel, string userId);
    }
}
