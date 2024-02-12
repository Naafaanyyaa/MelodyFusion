using MelodyFusion.BLL.Interfaces;
using MelodyFusion.BLL.Models.Request;
using MelodyFusion.DLL.Interfaces;
using System.Linq.Expressions;
using MelodyFusion.DLL.Entities;
using MelodyFusion.BLL.Models.Response.Statistic;

namespace MelodyFusion.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAuthenticationStatisticRepository _authenticationStatisticRepository;

        public StatisticService(ISubscriptionRepository subscriptionRepository, IAuthenticationStatisticRepository authenticationStatisticRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _authenticationStatisticRepository = authenticationStatisticRepository;
        }

        public async Task<SubscriptionStatisticResponse> GetSubscriptionStatisticAsync(StatisticRequest request)
        {
            var subscriptions = await _subscriptionRepository.GetAsync(
                predicate: sub => sub.CreatedDate >= request.DateFrom && sub.CreatedDate <= request.DateTo,
                includes: new List<Expression<Func<SubscriptionDto, object>>>()
                {
                    x => x.User
                },
                disableTracking: true
            );

            var groupedSubscriptions = subscriptions
                .GroupBy(sub => new {Year = sub.CreatedDate.Year, Month = sub.CreatedDate.Month})
                .OrderBy(grp => grp.Key.Year)
                .ThenBy(grp => grp.Key.Month)
                .Select(grp => new
                {
                    DateFrom = new DateTime(grp.Key.Year, grp.Key.Month, 1),
                    DateTo = new DateTime(grp.Key.Year, grp.Key.Month, DateTime.DaysInMonth(grp.Key.Year, grp.Key.Month)),
                    Count = grp.Count(),
                    TotalAmount = grp.Sum(sub => sub.Amount),
                    UniqueUserCount = grp.Select(sub => sub.UserId).Distinct().Count()
                })
                .ToList();

            var statisticResponse = new SubscriptionStatisticResponse
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                MonthlyCounts = groupedSubscriptions
                    .Select(grp => new MonthlySubscriptionCount
                    {
                        DateFrom = grp.DateFrom,
                        DateTo = grp.DateTo,
                        Count = grp.Count,
                        TotalAmount = grp.TotalAmount
                    })
                    .ToList(),
                TotalSubscriptions = subscriptions.Count,
                TotalAmount = subscriptions.Sum(sub => sub.Amount)
            };

            return statisticResponse;
        }

        public async Task<LoginStatisticResponse> GetLoginStatistic(StatisticRequest request)
        {
            List<AuthenticationStatisticDto> loginStatistic = await _authenticationStatisticRepository.GetAsync(
                predicate: x => x.CreatedDate >= request.DateFrom && x.CreatedDate <= request.DateTo,
                includeString: null,
                disableTracking: true);

            var groupedLogins = loginStatistic
                .GroupBy(sub => new { Year = sub.CreatedDate.Year, Month = sub.CreatedDate.Month })
                .OrderBy(grp => grp.Key.Year)
                .ThenBy(grp => grp.Key.Month)
                .Select(grp => new
                {
                    DateFrom = new DateTime(grp.Key.Year, grp.Key.Month, 1),
                    DateTo = new DateTime(grp.Key.Year, grp.Key.Month, DateTime.DaysInMonth(grp.Key.Year, grp.Key.Month)),
                    TotalLogins = grp.Count(x => x.IsAuthenticated),
                    TotalRegistrations = grp.Count(x => !x.IsAuthenticated)
                })
                .ToList();

            var result = new LoginStatisticResponse()
            {
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                MonthlyLoginCount = groupedLogins
                    .Select(grp => new MonthlyLoginCount()
                    {
                        DateFrom = grp.DateFrom,
                        DateTo = grp.DateTo,
                        MonthTotalLogins = grp.TotalLogins,
                        MonthTotalRegistrations = grp.TotalRegistrations
                    })
                    .ToList(),
                TotalLogins = loginStatistic.Count(x => x.IsAuthenticated),
                TotalRegistrations = loginStatistic.Count(x => !x.IsAuthenticated),

            };

            return result;
        }
    }
}
