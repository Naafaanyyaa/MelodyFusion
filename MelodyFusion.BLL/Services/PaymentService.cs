using AutoMapper;
using Braintree;
using MelodyFusion.BLL.Exceptions;
using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response;
using MelodyFusion.DLL.Entities;
using MelodyFusion.DLL.Interfaces;

namespace MelodyFusion.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBraintreeService _braintreeService;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public PaymentService(IBraintreeService braintreeService, ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _braintreeService = braintreeService;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public async Task<SubscriptionResponse> PayAsync(PaymentRequest requestModel, string userId)
        {
            if (!await IsSubscribed(userId))
            {
                throw new ValidationException("User already has subscription");
            }

            var transaction = await CreateTransaction(requestModel, userId);

            var result = await SaveTransactionToDb(transaction, userId);

            return result;
        }

        private async Task<bool> IsSubscribed(string userId)
        {
            List<SubscriptionDto> subscriptionsList = await _subscriptionRepository.GetAsync(
                predicate: x => x.UserId == userId,
                orderBy: q => q.OrderBy(x => x.CreatedDate),
                includeString: null);

            SubscriptionDto? lastSubscription = subscriptionsList.FirstOrDefault();

            return (DateTime.UtcNow - lastSubscription.CreatedDate).TotalDays < 30;
        }

        private async Task<Result<Transaction>> CreateTransaction(PaymentRequest requestModel, string userId)
        {
            IBraintreeGateway gateway = _braintreeService.GetGateway();

            TransactionRequest transactionRequest = new TransactionRequest
            {
                Amount = requestModel.Amount,
                PaymentMethodNonce = requestModel.PaymentMethodNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = await gateway.Transaction.SaleAsync(transactionRequest);

            return result;
        }

        private async Task<SubscriptionResponse> SaveTransactionToDb(Result<Transaction> transaction, string userId)
        {
            if (!transaction.IsSuccess())
            { 
                throw new ValidationException($"Transaction is not successful. {transaction.Message}");
            }

            SubscriptionDto? subscription = _mapper.Map<Result<Transaction>, SubscriptionDto>(transaction);

            subscription.UserId = userId;

            SubscriptionDto entityDto = await _subscriptionRepository.AddAsync(subscription);

            SubscriptionResponse? result = _mapper.Map<SubscriptionDto, SubscriptionResponse>(entityDto);

            return result;
        }
    }
}
