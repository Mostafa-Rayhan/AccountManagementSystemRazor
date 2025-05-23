namespace AccountManagementSystem.Models.Voucher
{
    public class Voucher
    {
        public int VoucherId { get; set; }
        public string VoucherType { get; set; }
        public DateTime VoucherDate { get; set; }
        public string ReferenceNo { get; set; }
        public string Narration { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<VoucherDetail> Details { get; set; }
    }
}
