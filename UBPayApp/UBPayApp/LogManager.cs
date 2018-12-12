using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UBPayApp
{
    class LogManager
    {
        public static object locker = new object();

        //日志文件所在路径
        private static string logPath = string.Empty;
        /// <summary>
        /// 保存日志的文件夹
        /// </summary>
        public static string LogPath
        {
            get
            {
                if (logPath == string.Empty)
                {
                    logPath = AppDomain.CurrentDomain.BaseDirectory + @"log\";
                }
                return logPath;
            }
            set { logPath = value; }
        }

        //------------
        /// <summary>
        /// 写日志
        /// <param name="logType">日志类型</param>
        /// <param name="msg">日志内容</param> 
        /// </summary>
        public static void WriteLogTran(string logType, string buf, string msg)
        {
            lock (locker)
            {
                System.IO.StreamWriter sw = null;
                try
                {
                    //同一天同一类日志以追加形式保存
                    sw = System.IO.File.AppendText(
                        LogPath + DateTime.Now.ToString("yyyyMMdd") + "_Tran.Log"
                        );
                    sw.WriteLine("[" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString("000") + "] [" + logType + "] [" + Var.AppVer + "] " + buf + " " + msg);
                }
                catch
                { }
                finally
                {
                    if (sw != null)
                        sw.Close();
                }
            }
        }

        public static void WriteLogTran(string logType, string buf, int msg)
        {
            System.IO.StreamWriter sw = null;
            try
            {
                //同一天同一类日志以追加形式保存
                sw = System.IO.File.AppendText(
                    LogPath + DateTime.Now.ToString("yyyyMMdd") + "_Tran.Log"
                    );
                sw.WriteLine("[" + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString("000") + "] [" + logType + "] [" + Var.AppVer + "] " + buf + " " + msg.ToString());
            }
            catch
            { }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        /// <summary>
        /// 写日志
        /// </summary>
        public static void WriteLogTran(LogType logType, string buf, string msg)
        {
            WriteLogTran(logType.ToString(), buf, msg);
        }


    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        Trace,      // 堆栈跟踪信息
        Warning,    // 警告信息
        Error,      // 错误信息应该包含对象名、发生错误点所在的方法名称、具体错误信息
        Message     // 信息
    }
}
