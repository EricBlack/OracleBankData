using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OracleBankData
{
    public class RecordSqlInfo
    {
        private static string SuccessFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "success.txt");
        private static string FailFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "fail.txt");

        private StreamWriter successWriter;
        private StreamWriter failWriter;

        private static readonly object InitObject = new object();
        private static readonly object WriteObject = new object();

        public void InitRecordWriter()
        {
            try
            {
                lock (InitObject)
                {
                    InitFile();
                    successWriter = new StreamWriter(SuccessFile, true, Encoding.UTF8);
                    failWriter = new StreamWriter(FailFile, true, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                $"Init record write got exception: {ex.Message}".WriteErrorLine();
            }
        }

        public void SuccessRecord(string sqlText)
        {
            Write(successWriter, sqlText, true);
        }

        public void FailRecord(string sqlText)
        {
            Write(failWriter, sqlText, false);
        }

        private void Write(StreamWriter writer, string content, bool success)
        {
            const string timeStamp = "yyyy-MM-dd HH:mm:ss.fff";
            lock(WriteObject)
            {
                try
                {
                    var text = $"[{DateTime.Now.ToString(timeStamp)}]: " + content;
                    if (success)
                    {
                        text.WriteSuccessLine();
                    }
                    else
                    {
                        text.WriteErrorLine();
                    }
                    
                    writer.WriteLine(text);
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    $"Write record got exception: {ex.Message}".WriteErrorLine();
                }
            }
        }

        private void InitFile()
        {
            if (File.Exists(SuccessFile))
            {
                File.Delete(SuccessFile);
            }
            File.Create(SuccessFile);

            if (File.Exists(FailFile))
            {
                File.Delete(FailFile);
            }
            File.Create(FailFile);
        }
    }
}
