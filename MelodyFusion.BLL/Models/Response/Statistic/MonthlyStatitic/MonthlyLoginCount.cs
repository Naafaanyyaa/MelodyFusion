namespace MelodyFusion.BLL.Models.Response.Statistic
{
    public class MonthlyLoginCount
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? MonthTotalLogins { get; set; }
        public int? MonthTotalRegistrations { get; set; }
    }
}
