namespace MelodyFusion.BLL.Models.Response.Statistic
{
    public class SubscriptionStatisticResponse
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; } = DateTime.UtcNow;
        public List<MonthlySubscriptionCount>? MonthlyCounts { get; set; }
        public int? TotalSubscriptions { get; set; }
        public decimal? TotalAmount { get; set; }
    }

}
