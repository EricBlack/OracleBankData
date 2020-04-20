using System;

namespace OracleBankData
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = DbConfig.ConfigInstance;
            Console.WriteLine(config);
        }
    }
}
