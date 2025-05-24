using AccountManagementSystem.Models.Account;
using AccountManagementSystem.Models.Voucher;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AccountManagementSystem.Pages.Voucher
{
    [Authorize(Roles = "Admin,Accountant")]
    public class CreateModel : PageModel
    {
        private readonly VoucherService _voucherService;
        private readonly AccountService _accountService;

        public CreateModel(VoucherService voucherService, AccountService accountService)
        {
            _voucherService = voucherService;
            _accountService = accountService;
        }

        [BindProperty]
        public VoucherInputModel Voucher { get; set; }

        public List<ChartOfAccount> Accounts { get; set; }

        public class VoucherInputModel
        {
            [Required]
            public string VoucherType { get; set; }

            [Required]
            public DateTime VoucherDate { get; set; } = DateTime.Today;

            [Required]
            public string ReferenceNo { get; set; }

            public string Narration { get; set; }

            [BindProperty]
            public List<VoucherDetailInputModel> Details { get; set; } = new();
        }

        public class VoucherDetailInputModel
        {
            [Required]
            public int AccountId { get; set; }

            [Required]
            [Range(0.01, double.MaxValue)]
            public decimal Amount { get; set; }

            [Required]
            public bool IsDebit { get; set; }
        }

        public void OnGet()
        {
            Accounts = _accountService.GetAccountTree();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Accounts = _accountService.GetAccountTree();
                return Page();
            } 

            var voucher = new Voucher
            {
                VoucherType = Voucher.VoucherType,
                VoucherDate = Voucher.VoucherDate,
                ReferenceNo = Voucher.ReferenceNo,
                Narration = Voucher.Narration,
                CreatedBy = User.Identity.Name,
                Details = Voucher.Details.Select(d => new VoucherDetail
                {
                    AccountId = d.AccountId,
                    Amount = d.Amount,
                    IsDebit = d.IsDebit
                }).ToList()
            };

            var voucherId = _voucherService.SaveVoucher(voucher, voucher.Details);

            return RedirectToPage("./Details", new { id = voucherId });
        }
    }
}