namespace MelodyFusion.BLL.Models.Response.Statistic
{
    public class LoginStatisticResponse
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; } = DateTime.UtcNow;
        public List<MonthlyLoginCount>? MonthlyLoginCount { get; set; }
        public int? TotalLogins { get; set; }
        public int? TotalRegistrations { get; set; }
    }
}
