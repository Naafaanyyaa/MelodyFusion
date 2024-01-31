using Braintree;
using MelodyFusion.BLL.Models.Request;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IBraintreeService
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
