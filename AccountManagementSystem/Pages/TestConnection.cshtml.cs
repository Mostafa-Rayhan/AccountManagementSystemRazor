using AccountManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AccountManagementSystem.Pages
{
    public class TestConnectionModel : PageModel
    {
        private readonly DatabaseHelper _dbHelper;

        public TestConnectionModel(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public string Message { get; set; }

        public void OnGet()
        {
            try
            {
                var parameters = new SqlParameter[] { };
                //var result = _dbHelper.ExecuteStoredProcedure("sp_GetAccountTree", parameters);
                //Message = $"Connection successful! Found {result.Rows.Count} accounts.";
            }
            catch (Exception ex)
            {
                Message = $"Connection failed: {ex.Message}";
            }
        }
    }
}
