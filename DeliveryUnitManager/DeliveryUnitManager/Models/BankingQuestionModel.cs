namespace DeliveryUnitManager.Models
{
    public class BankingQuestionModel
    {
        public long Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string PlatformType { get; set; }
        public string Level { get; set; }
        public string Projects { get; set; }
    }
}
