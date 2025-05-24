using AccountManagementSystem.Models.Account;
using AccountManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AccountManagementSystem.Pages.Account
{
    [Authorize(Roles = "Admin,Accountant,Viewer")]
    public class IndexModel : PageModel
    {
        private readonly AccountService _accountService;

        public IndexModel(AccountService accountService)
        {
            _accountService = accountService;
        }

        public List<ChartOfAccount> AccountTree { get; set; }

        public void OnGet()
        {
            AccountTree = _accountService.GetAccountTree();
        }
    }
}