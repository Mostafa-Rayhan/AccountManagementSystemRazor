using AccountManagementSystem.Data;
using AccountManagementSystem.Models.Voucher;
using System.Data;
using System.Data.SqlClient;

namespace AccountManagementSystem.Services
{
    public class VoucherService
    {
        private readonly DatabaseHelper _dbHelper;

        public VoucherService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public int SaveVoucher(Voucher voucher, List<VoucherDetail> details)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@VoucherType", voucher.VoucherType),
                new SqlParameter("@VoucherDate", voucher.VoucherDate),
                new SqlParameter("@ReferenceNo", voucher.ReferenceNo),
                new SqlParameter("@Narration", voucher.Narration ?? (object)DBNull.Value),
                new SqlParameter("@CreatedBy", voucher.CreatedBy)
            };

            var detailsTable = new DataTable();
            detailsTable.Columns.Add("AccountId", typeof(int));
            detailsTable.Columns.Add("Amount", typeof(decimal));
            detailsTable.Columns.Add("IsDebit", typeof(bool));

            foreach (var detail in details)
            {
                detailsTable.Rows.Add(detail.AccountId, detail.Amount, detail.IsDebit);
            }

            var detailsParam = new SqlParameter("@Details", detailsTable)
            {
                TypeName = "dbo.VoucherDetailType",
                SqlDbType = SqlDbType.Structured
            };

            var allParameters = parameters.Concat(new[] { detailsParam }).ToArray();

            var result = _dbHelper.ExecuteStoredProcedure("sp_SaveVoucher", allParameters);

            return Convert.ToInt32(result.Rows[0]["NewVoucherId"]);
        }

        public DataTable GetVoucherReport(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var parameters = new List<SqlParameter>();

            if (fromDate.HasValue)
                parameters.Add(new SqlParameter("@FromDate", fromDate));

            if (toDate.HasValue)
                parameters.Add(new SqlParameter("@ToDate", toDate));

            return _dbHelper.ExecuteStoredProcedure("sp_GetVoucherReport", parameters.ToArray());
        }
    }
}