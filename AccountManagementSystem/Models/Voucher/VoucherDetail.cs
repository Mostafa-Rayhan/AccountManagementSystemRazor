namespace AccountManagementSystem.Models.Voucher
{
    public class VoucherDetail
    {
        public int VoucherDetailId { get; set; }
        public int VoucherId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public bool IsDebit { get; set; }
    }
}
