using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using FaPA.AppServices;

namespace FaPA.DomainServices.Utils
{
    public static class SqlActionUtil
    {

        public static string RunSql(string sqlQuery, int timeout = 30)
        {
            var result = string.Empty;

            try
            {
                var commandText = sqlQuery.Split(new string[] { String.Format("{0}GO{0}", Environment.NewLine) },
                    StringSplitOptions.RemoveEmptyEntries);

                using (var connectionSql = new SqlConnection(StoreAccess.ConnString))
                {
                    var commandSql = new SqlCommand("~", connectionSql);
                    commandSql.CommandType = CommandType.Text;
                    commandSql.Connection.Open();

                    const string comdCompletedSuccessfully = "Command(s) completed successfully.";

                    result = comdCompletedSuccessfully;

                    foreach (var t in commandText.Where(t => t.Trim().Length > 0))
                    {
                        try
                        {
                            var sqlText = t;
                            commandSql.CommandText = sqlText;
                            commandSql.CommandTimeout = timeout;
                            commandSql.ExecuteNonQuery();

                            result = comdCompletedSuccessfully;
                        }
                        catch (SqlException ex)
                        {
                            result = String.Format("Failed: {0}", ex.Message);

                            Debug.Print(t);

                            Debug.Print(result);

                            break;
                        }
                    }

                    commandSql.Connection.Close();
                }


            }
            catch (SqlException ex)
            {
                result = ex.Message;
            }

            return result;
        }
    }
}
