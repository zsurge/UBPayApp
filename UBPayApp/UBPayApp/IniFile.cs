using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

namespace UBPayApp
{
    class IniFile
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        //section：要读取的段落名
        //key: 要读取的键
        //defVal: 读取异常的情况下的缺省值
        //retVal: key所对应的值，如果该key不存在则返回空值
        //size: 值允许的大小
        //filePath: INI文件的完整路径和文件名

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //section: 要写入的段落名
        //key: 要写入的键，如果该key存在则覆盖写入
        //val: key所对应的值
        //filePath: INI文件的完整路径和文件名

        public string Path;
        public IniFile(string path)
        {
            this.Path = path;
        }

        /**/
        /// <summary>
        /// 写INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <param name="iValue">值</param>
        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, this.Path);
        }

        /**/
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">段落</param>
        /// <param name="key">键</param>
        /// <returns>返回的键值</returns>
        public string IniReadValue(string section, string key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(10240);

                int i = GetPrivateProfileString(section, key, "", temp, 10240, this.Path);
                return temp.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        /**/
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="Section">段，格式[]</param>
        /// <param name="Key">键</param>
        /// <returns>返回byte类型的section组或键值组</returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];

            int i = GetPrivateProfileString(section, key, "", temp, 255, this.Path);
            return temp;
        }
    }

}
