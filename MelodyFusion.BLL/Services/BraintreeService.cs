using Braintree;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using Microsoft.Extensions.Configuration;

namespace MelodyFusion.BLL.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IConfiguration _config;

        public BraintreeService(IConfiguration config)
        {
            _config = config;
        }
        public IBraintreeGateway CreateGateway()
        {
            var newGateway = new BraintreeGateway()
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = _config.GetValue<string>("BraintreeGateway:MerchantId"),
                PublicKey = _config.GetValue<string>("BraintreeGateway:PublicKey"),
                PrivateKey = _config.GetValue<string>("BraintreeGateway:PrivateKey")
            };

            return newGateway;
        }

        public IBraintreeGateway GetGateway()
        {
            return CreateGateway();
        }

        public async Task<Result<Transaction>> CreateTransaction(PaymentRequest requestModel)
        {
            var gateway = GetGateway();
            var request = new TransactionRequest
            {
                Amount = requestModel.Amount,
                PaymentMethodNonce = requestModel.PaymentMethodNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = await gateway.Transaction.SaleAsync(request);
        }
    }
}
