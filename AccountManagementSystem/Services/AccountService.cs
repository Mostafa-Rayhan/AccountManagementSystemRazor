using AccountManagementSystem.Data;
using AccountManagementSystem.Models.Account;
using System.Data;
using System.Data.SqlClient;

namespace AccountManagementSystem.Services
{
    public class AccountService
    {
        private readonly DatabaseHelper _dbHelper;

        public AccountService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<ChartOfAccount> GetAccountTree()
        {
            var parameters = new SqlParameter[] { };
            var dataTable = _dbHelper.ExecuteStoredProcedure("sp_GetAccountTree", parameters);

            return dataTable.AsEnumerable().Select(row => new ChartOfAccount
            {
                AccountId = row.Field<int>("AccountId"),
                AccountCode = row.Field<string>("AccountCode"),
                AccountName = row.Field<string>("AccountName"),
                ParentAccountId = row.Field<int?>("ParentAccountId"),
                AccountType = row.Field<string>("AccountType"),
                IsActive = row.Field<bool>("IsActive"),
                Level = row.Field<int>("Level")
            }).ToList();
        }

        public int ManageAccount(string action, ChartOfAccount account)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", action),
                new SqlParameter("@AccountId", account.AccountId),
                new SqlParameter("@AccountCode", account.AccountCode),
                new SqlParameter("@AccountName", account.AccountName),
                new SqlParameter("@ParentAccountId", account.ParentAccountId ?? (object)DBNull.Value),
                new SqlParameter("@AccountType", account.AccountType),
                new SqlParameter("@IsActive", account.IsActive)
            };

            var result = _dbHelper.ExecuteStoredProcedure("sp_ManageChartOfAccounts", parameters);

            return action == "INSERT"
                ? Convert.ToInt32(result.Rows[0]["NewAccountId"])
                : account.AccountId;
        }

        public ChartOfAccount GetAccountDetails(int accountId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@AccountId", accountId)
            };

            var dataTable = _dbHelper.ExecuteStoredProcedure("sp_GetAccountDetails", parameters);

            return dataTable.AsEnumerable().Select(row => new ChartOfAccount
            {
                AccountId = row.Field<int>("AccountId"),
                AccountCode = row.Field<string>("AccountCode"),
                AccountName = row.Field<string>("AccountName"),
                ParentAccountId = row.Field<int?>("ParentAccountId"),
                AccountType = row.Field<string>("AccountType"),
                IsActive = row.Field<bool>("IsActive")
            }).FirstOrDefault();
        }
    }
}
