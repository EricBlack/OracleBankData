using System;
using System.Collections.Generic;
using System.Text;

namespace OracleBankData
{
    public class DbConfig
    {
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
        public string DbName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"Data Source={ServerHost}:{ServerPort}/{DbName};User Id={UserName};Password={Password}";
        }

        public override string ToString()
        {
            return $"ServerHost={ServerHost}, ServerPort={ServerPort}, DbName={DbName}, UserName={UserName}, Password={Password}";
        }

        public static DbConfig ConfigInstance => ConfigHelper<DbConfig>.GetConfigInfo("db.json");
    }
}
