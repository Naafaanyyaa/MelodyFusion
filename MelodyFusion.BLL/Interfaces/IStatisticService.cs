using MelodyFusion.BLL.Models.Request;
using MelodyFusion.BLL.Models.Response.Statistic;

namespace MelodyFusion.BLL.Interfaces
{
    public interface IStatisticService
    {
        Task<SubscriptionStatisticResponse> GetSubscriptionStatisticAsync(StatisticRequest request);
        Task<LoginStatisticResponse> GetLoginStatistic(StatisticRequest request);
    }
}
