using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace OracleBankData
{
    public class OracleHelper
    {
        public DbConfig DbConfig { get; set; }
        public RecordSqlInfo Recorder { get; set; }

        public OracleHelper()
        {
            DbConfig = DbConfig.ConfigInstance;
            Recorder = new RecordSqlInfo();
        }

        public List<object> QueryResults(string sql)
        {
            using (OracleConnection con = new OracleConnection(DbConfig.GetConnectionString()))
            {
                var result = new List<object>();
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        con.Open();
                        cmd.BindByName = true;

                        //Use the command to display employee names from 
                        // the EMPLOYEES table
                        cmd.CommandText = sql;

                        // Assign id to the department number 50 
                        OracleParameter id = new OracleParameter("id", 50);
                        cmd.Parameters.Add(id);

                        //Execute the command and use DataReader to display the data
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(reader.GetString(0));
                        }

                        reader.Dispose();

                        //record success sql text
                        Recorder.SuccessRecord(sql);

                    }
                    catch (Exception ex)
                    {
                        $"QueryResults got exception: {ex.Message}".WriteErrorLine();
                        //record failed sql text
                        Recorder.FailRecord(sql);
                    }

                    return result;
                }
            }
        }
    }
}
