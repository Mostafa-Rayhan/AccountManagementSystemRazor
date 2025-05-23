namespace AccountManagementSystem.Models.Account
{
    public class ChartOfAccount
    {
        public int AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public int? ParentAccountId { get; set; }
        public string AccountType { get; set; }
        public bool IsActive { get; set; }
        public ChartOfAccount ParentAccount { get; set; }
        public int Level { get; set; }
    }
}
