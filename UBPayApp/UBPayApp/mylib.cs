using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace UBPayApp
{
    //2012-05-21
    class MYLIB
    {
        public char tohex(char b)
        {
            if (b > 0x2f && b < 0x3a) return (char)(b - 0x30);
            else if (b == 'a' || b == 'A') return (char)(0xA);
            else if (b == 'b' || b == 'B') return (char)(0xB);
            else if (b == 'c' || b == 'C') return (char)(0xC);
            else if (b == 'd' || b == 'D') return (char)(0xD);
            else if (b == 'e' || b == 'E') return (char)(0xE);
            else if (b == 'f' || b == 'F') return (char)(0xF);
            else return (char)0x00;
        }

        // 所有字节的和，按256取模，再取补，得到的值
        public int CalcCRC(byte[] data, int len)
        {
            int i;
            byte aaa = 0;

            for (i = 0; i < len; i++)
                aaa += data[i];
            aaa = (byte)(aaa % 256);
            aaa = (byte)(~aaa);
            aaa = (byte)(aaa + 1);
            return (byte)aaa;
        }

        //Only copy 0~9 a~f A~F
        private int RemoveBlank(byte[] Src, byte[] Dest, int iLen)
        {
            int j = 0;
            for (int i = 0; i < iLen; i++)
            {
                if ((Src[i] >= '0' && Src[i] <= '9') || (Src[i] >= 'a' && Src[i] <= 'f') || (Src[i] >= 'A' && Src[i] <= 'F')) Dest[j++] = Src[i];
            }
            return j;
        }

        //"12 34 56 78" -> 0x12 0x34 0x56 0x78
        public int GetHexFromString(string Src, byte[] Dest)
        {
            byte[] srcbuff = Encoding.Default.GetBytes(Src);
            byte[] destbuff = new byte[Src.Length + 1];

            int len = RemoveBlank(srcbuff, destbuff, Src.Length);

            CompressString(destbuff, Dest, len);

            return (len / 2);
        }

        //"12345678" -> 0x12 0x34 0x56 0x78
        public int CompressString(byte[] cpSrc, byte[] cpDes, int iLen)
        {
            int iLoop = 0, i = 0;
            byte cTmp1 = 0;
            byte cTmp2 = 0;

            byte cA = 0;
            byte cB = 0;

            byte[] cpBuf = new byte[iLen + 1];

            for (iLoop = 0, i = 0; iLoop < iLen; i++)
            {
                cA = cpSrc[iLoop];
                cB = cpSrc[iLoop + 1];

                if ((cA <= 0x3F) && (cA >= 0x30))
                    cTmp1 = (byte)(cA - 0x30);
                if ((cA <= 0x46) && (cA >= 0x41))
                    cTmp1 = (byte)(cA - 0x37);
                if ((cA <= 0x66) && (cA >= 0x61))
                    cTmp1 = (byte)(cA - 0x57);

                if ((cB <= 0x3F) && (cB >= 0x30))
                    cTmp2 = (byte)(cB - 0x30);
                if ((cB <= 0x46) && (cB >= 0x41))
                    cTmp2 = (byte)(cB - 0x37);
                if ((cB <= 0x66) && (cB >= 0x61))
                    cTmp2 = (byte)(cB - 0x57);

                cpBuf[i] = (byte)(cTmp1 * 16 + cTmp2);
                iLoop = iLoop + 2;
            }
            Array.Copy(cpBuf, cpDes, iLen / 2);

            return 0;
        }

        //0x12 0x34 0x56 0x78 -> "12345678"
        public int SplitString(byte[] cpSrc, byte[] cpDes, int iLen)
        {
            byte ucTmp = 0;
            byte w = 0;
            int i = 0;
            int j = 0;

            for (w = 0; w < iLen; w++)
            {
                ucTmp = (byte)((cpSrc[j]) >> 4);
                if (ucTmp > 9)
                    cpDes[i] = (byte)(ucTmp + 55);
                else
                    cpDes[i] = (byte)(ucTmp + 0x30);

                i++;
                ucTmp = (byte)((cpSrc[j]) % 0x10);
                if (ucTmp > 9)
                    cpDes[i] = (byte)(ucTmp + 55);
                else
                    cpDes[i] = (byte)(ucTmp + 0x30);

                i++;
                j++;
            }
            return 0;
        }

        public string FormatFskCid(byte[] CidBuf, int len)
        {
            int MsgTypeCnt = 0;//消息类型所在缓存中的偏移量
            int i = 0, j = 0;
            int Len = 0;
            byte ParityBit = 0;
            int TmpLen = 0;
            byte[] buf = new byte[1024];
            byte[] CidName = new byte[20];
            byte[] CidTime = new byte[20];
            byte[] CidNumber = new byte[20];
            string m_OutBox = null;

            for (MsgTypeCnt = 0; MsgTypeCnt < len; MsgTypeCnt++)//查找消息类型所在缓存中的偏移量
            {
                if ((CidBuf[MsgTypeCnt] == 0x04) || (CidBuf[MsgTypeCnt] == 0x80))
                {
                    break;
                }
                else if (i++ > 10) return "无0x04或0x80";//查找超过10次后，return 0，认为无有效数据
            }

            if (CidBuf[MsgTypeCnt] == 0x04)//单数据消息格式
            {
                m_OutBox += "单数据消息格式:\r\n";

                Len = CidBuf[MsgTypeCnt + 1];
                //strncpy((char *)buf, (char *)(CidBuf+MsgTypeCnt), 1+1+Len+1);//MsgType+Len+Data+ParityBit
                Array.Copy(CidBuf, MsgTypeCnt, buf, 0, 1 + 1 + Len + 1);

                ParityBit = (byte)CalcCRC(buf, 1 + 1 + Len);//MsgType+Len+Data
                if ((ParityBit & 0x00FF) != buf[1 + 1 + Len])
                {
                    return "单数据消息格式校验错误!";//ParityBit Error
                }

                for (i = 0; i < Len; i++)
                {
                    if (i < 8)//time/date
                    {
                        CidTime[i] = buf[1 + 1 + i];//MsgType+Len
                    }
                    else if (i >= 8)//number
                    {
                        CidNumber[j] = buf[1 + 1 + 8 + j];//MsgType+Len+8(time/date)
                        j++;
                    }
                }
                m_OutBox += "Time:";
                m_OutBox += Encoding.Default.GetString(CidTime);
                m_OutBox += "\r\n";

                m_OutBox += "Number:";
                m_OutBox += Encoding.Default.GetString(CidNumber);
                m_OutBox += "\r\n";

            }
            else if (CidBuf[MsgTypeCnt] == 0x80)//复合数据消息格式
            {
                m_OutBox += "复合数据消息格式:\r\n";

                Len = CidBuf[MsgTypeCnt + 1];
                //strncpy((char *)buf, (char *)(CidBuf+MsgTypeCnt), 1+1+Len+1);//MsgType+ Len+ Data+ ParityBit
                Array.Copy(CidBuf, MsgTypeCnt, buf, 0, 1 + 1 + Len + 1);

                ParityBit = (byte)CalcCRC(buf, 1 + 1 + Len);//MsgType+Len+Data
                if ((ParityBit & 0x00FF) != buf[1 + 1 + Len])
                {
                    return "复合数据消息格式校验错误!";//ParityBit Error
                }

                for (i = 0; i < Len; i++)
                {
                    if (buf[i] == 0x01)// time/date max 8
                    {
                        TmpLen = buf[i + 1];
                        if (TmpLen == 8)//strncpy((char *)CidTime, (char *)(buf+i+1+1), TmpLen);//save time
                            Array.Copy(buf, i + 1 + 1, CidTime, 0, TmpLen);
                        //else return 3;//数据长度错误

                        TmpLen = 0;
                    }
                    else if (buf[i] == 0x02)// 主叫号码 max 30
                    {
                        TmpLen = buf[i + 1];
                        if (TmpLen <= 30)//strncpy((char *)CidNumber, (char *)(buf+i+1+1), TmpLen);//save number
                            Array.Copy(buf, i + 1 + 1, CidNumber, 0, TmpLen);
                        //else return 3;//数据长度错误

                        TmpLen = 0;
                    }
                    else if (buf[i] == 0x04)// 无主叫号码的原因 max 1
                    {
                        TmpLen = buf[i + 1];
                        if (TmpLen == 1)
                        {
                            if (buf[i + 1 + TmpLen] == 'O')//strncpy((char *)CidNumber, "无主叫号码", strlen("无主叫号码"));
                                Array.Copy(Encoding.Default.GetBytes("无主叫号码"), CidNumber, 10);
                            else if (buf[i + 1 + TmpLen] == 'P')//strncpy((char *)CidNumber, "主叫号码受限", strlen("主叫号码受限"));
                                Array.Copy(Encoding.Default.GetBytes("主叫号码受限"), CidNumber, 12);
                        }

                        TmpLen = 0;
                    }
                    else if (buf[i] == 0x07)// 主叫姓名 max 50
                    {
                        TmpLen = buf[i + 1];
                        if (TmpLen <= 50)//strncpy((char *)CidName, (char *)(buf+i+1+1), TmpLen);//
                            Array.Copy(buf, i + 1 + 1, CidName, 0, TmpLen);
                        //else return 3;//数据长度错误

                        TmpLen = 0;
                    }
                    else if (buf[i] == 0x08)// 无主叫姓名的原因 max 1
                    {
                        TmpLen = buf[i + 1];
                        if (TmpLen == 1)
                        {
                            if (buf[i + 1 + TmpLen] == 'O')//strncpy((char *)CidName, "无主叫姓名", strlen("无主叫姓名"));
                                Array.Copy(Encoding.Default.GetBytes("无主叫姓名"), CidName, 10);
                            else if (buf[i + 1 + TmpLen] == 'P')//strncpy((char *)CidName, "主叫姓名受限", strlen("主叫姓名受限"));
                                Array.Copy(Encoding.Default.GetBytes("主叫姓名受限"), CidName, 12);
                        }

                        TmpLen = 0;
                    }
                    else if (buf[i] == 0xE1)//信息 max 120
                    {
                        //return 3;//数据长度错误		
                    }
                }
                m_OutBox += "Time:";
                m_OutBox += Encoding.Default.GetString(CidTime);//CidTime.ToString();
                m_OutBox += "\r\n";

                m_OutBox += "Number:";
                m_OutBox += Encoding.Default.GetString(CidNumber);
                m_OutBox += "\r\n";

                m_OutBox += "Name:";
                m_OutBox += Encoding.Default.GetString(CidName);
                m_OutBox += "\r\n";

                //return 1;
            }
            else
            {
                m_OutBox = "未找到消息类型!";
            }
            //return 0;
            return m_OutBox;
        }

        //-------------------------------------------------------------------------------------------------------------
        private void AddEnterLine(string Path)
        {
            StreamReader StringReader;
            try
            {
                //get fp from path
                StringReader = new StreamReader(Path, Encoding.GetEncoding("gb2312"));
            }
            catch (Exception Error)
            {
                return;
            }

            //将所有的数据读出
            string AllString = StringReader.ReadToEnd();

            //close
            StringReader.Close();

            if (!AllString.EndsWith("\r\n"))
            {
                AllString += "\r\n";

                try
                {
                    //get fp from path
                    StreamWriter StringWriter = new StreamWriter(Path, false, Encoding.GetEncoding("gb2312"));
                    StringWriter.Write(AllString);
                    StringWriter.Close();
                }
                catch (Exception Error)
                {
                    return;
                }
            }
        }

        //ini reader
        private int mystrstr(string Src, string Dest)
        {
            int Offset = 0;
            if (Src.Length == 0) return -1;
            if (Src.Length < Dest.Length) return -1;

            for (Offset = 0; Offset < (Src.Length); Offset++)
            {
                if (System.String.Compare(Src, Offset, Dest, 0, Dest.Length) == 0)
                {
                    return Offset;
                }
            }
            return -1;
        }

        private int GetItemValue(string Src, int SrcOffset, char[] ItemGet, char[] ValueGet)//copy SrcOffset 到 0x0D 0x0A为止
        {
            int ItemLen = 0;
            int ValueLen = 0;

            //ItemGet=ValueGet
            for (ItemLen = 0; ; ItemLen++)
            {
                if (Src[SrcOffset + ItemLen] == '=')
                {
                    break;
                }
            }

            for (ValueLen = 0; ; ValueLen++)
            {
                try
                {
                    if (Src[SrcOffset + ItemLen + 1 + ValueLen] == 0x0D)
                    {
                        break;
                    }
                }
                catch (Exception Error)
                {
                    break;
                }
            }

            Src.CopyTo(SrcOffset, ItemGet, 0, ItemLen);
            Src.CopyTo(SrcOffset + ItemLen + 1, ValueGet, 0, ValueLen);

            return (ItemLen + 1 + ValueLen + 2);

        }

        public string INIRead(string ItemSrc, string Path)
        {
            //打开文件
            try
            {
                char[] ItemGet = new char[200];
                char[] ValueGet = new char[200];
                ItemGet.Initialize();
                ValueGet.Initialize();

                AddEnterLine(Path);

                //get fp from path
                StreamReader StringReader = new StreamReader(Path, Encoding.GetEncoding("gb2312"));

                //将所有的数据读出
                string AllString = StringReader.ReadToEnd();

                //close
                StringReader.Close();

                //找到ItemGet在AllString中的偏移量
                int Offset = mystrstr(AllString, ItemSrc);

                //if can't find ItemSrc
                if (Offset == -1)
                    return null;

                //get ItemGet and ValueGet
                int ItemValueLen = GetItemValue(AllString, Offset, ItemGet, ValueGet);

                //compare the ItemSrc with ItemGet
                string ItemDestString = new string(ItemGet);
                if (ItemDestString.CompareTo(ItemSrc) == 0)//equal, return result
                {
                    string ValueStr = new string(ValueGet);

                    int indexof = ValueStr.IndexOf('\0');
                    int len = ValueStr.Length;
                    string s1 = ValueStr.Remove(indexof, len - indexof);

                    return s1;
                }
                else
                {

                }
            }
            catch (Exception Error)
            {
                //MessageBox.Show(Error.Message, "//MessageBox", //MessageBoxButtons.OK, //MessageBoxIcon.Error);
                return null;
            }

            return null;
        }
        //-------------------------------------------------------------------------------------------------------------
        //ini writer
        private string[] StringSplit(string AllString, int SeparateOffset, int Len)
        {
            char[] Split0 = new char[SeparateOffset];
            char[] Split1 = new char[Len];
            char[] Split2 = new char[AllString.Length - SeparateOffset - Len];
            Split0.Initialize();
            Split1.Initialize();
            Split2.Initialize();

            AllString.CopyTo(0, Split0, 0, SeparateOffset);
            AllString.CopyTo(SeparateOffset, Split1, 0, Len);
            AllString.CopyTo(SeparateOffset + Len, Split2, 0, AllString.Length - SeparateOffset - Len);

            string[] s = new string[3];
            s[0] = new string(Split0);
            s[1] = new string(Split1);
            s[2] = new string(Split2);

            return s;
        }

        public bool INIWrite(string ItemSrc, string ValueSrc, string Path)
        {
            //打开文件
            try
            {
                char[] ItemGet = new char[200];
                char[] ValueGet = new char[200];
                ItemGet.Initialize();
                ValueGet.Initialize();

                AddEnterLine(Path);

                //get fp from path
                StreamReader StringReader = new StreamReader(Path, Encoding.GetEncoding("gb2312"));

                //read all string from ini file
                string AllString = StringReader.ReadToEnd();

                //close
                StringReader.Close();

                //get the Offset of ItemSrc in ini file
                int Offset = mystrstr(AllString, ItemSrc);

                //if can't find ItemSrc
                if (Offset == -1)
                    return false;

                //get ItemGet and ValueGet
                int ItemValueLen = GetItemValue(AllString, Offset, ItemGet, ValueGet);

                //compare the ItemSrc with ItemGet
                string ItemGetString = new string(ItemGet);
                string ValueGetString = new string(ValueGet);
                //equal, write the ValueSrc to ini
                if (ItemSrc.CompareTo(ItemGetString) == 0)
                {
                    //split AllString to three string
                    string[] s = StringSplit(AllString, Offset, ItemValueLen);

                    //modify the s[1]
                    s[1] = ItemSrc;
                    s[1] += '=';
                    s[1] += ValueSrc;
                    s[1] += "\r\n";

                    //Merge the three string
                    string Result = s[0];
                    Result += s[1];
                    Result += s[2];

                    try
                    {
                        //rewrite to ini file
                        StreamWriter StringWriter = new StreamWriter(Path, false, Encoding.GetEncoding("gb2312"));
                        StringWriter.Write(Result);
                        StringWriter.Close();
                    }
                    catch (Exception Error)
                    {
                        //MessageBox.Show(Error.Message, "//MessageBox", //MessageBoxButtons.OK, //MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception Error)
            {
                //MessageBox.Show(Error.Message, "//MessageBox", //MessageBoxButtons.OK, //MessageBoxIcon.Error);
            }

            return true;
        }

        public bool INIAdd(string ItemSrc, string ValueSrc, string Path)
        {
            //打开文件
            try
            {
                AddEnterLine(Path);

                //get fp from path
                StreamReader StringReader = new StreamReader(Path, Encoding.GetEncoding("gb2312"));

                //read all string from ini file
                string AllString = StringReader.ReadToEnd();

                //close
                StringReader.Close();

                AllString += ItemSrc + '=' + ValueSrc + "\r\n";

                try
                {
                    //rewrite to ini file
                    StreamWriter StringWriter = new StreamWriter(Path, false, Encoding.GetEncoding("gb2312"));
                    StringWriter.Write(AllString);
                    StringWriter.Close();
                }
                catch (Exception Error)
                {
                    //MessageBox.Show(Error.Message, "//MessageBox", //MessageBoxButtons.OK, //MessageBoxIcon.Error);
                    return false;
                }

            }
            catch (Exception Error)
            {
                //MessageBox.Show(Error.Message, "//MessageBox", //MessageBoxButtons.OK, //MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public UInt32 BytesToUINT32(byte[] buff, int len)
        {
            UInt32 value = 0;

            for (int i = 0; i < len; i++)
            {
                value <<= 8;
                value |= buff[i];

            }

            return value;
        }

        public UInt16 GetSum(byte[] buff, UInt32 len)
        {
            UInt16 sum = 0;
            UInt32 i = 0;
            for (i = 0; i < len; i++)
            {
                sum += buff[i];
            }
            return sum;
        }

        //"112233" -> 0x11
        public UInt32 GetUINT32FromIni(string Item, string Path)
        {
            string ItemString = INIRead(Item, Path);
            byte[] ItemBytes = new byte[10];
            int ItemLen = 0;
            ItemBytes.Initialize();

            if (ItemString != null)
            {
                ItemLen = GetHexFromString(ItemString, ItemBytes);
            }
            else
            {
                return 0;
            }

            UInt32 ItemValue = BytesToUINT32(ItemBytes, ItemLen);

            return ItemValue;
        }

        //"11223344" -> 0x11223344
        public int GetIntFromIni(string Item, string Path)
        {
            string ItemString = INIRead(Item, Path);
            byte[] ItemBytes = new byte[10];
            ItemBytes.Initialize();

            if (ItemString != null)
            {
                int ItemLen = GetHexFromString(ItemString, ItemBytes);
            }
            else
            {
                return 0xFFFFFFF;
            }

            int ItemValue = ItemBytes[0];

            return ItemValue;
        }

        // 将字符串 压缩 成BCD码   如：12345678 转换后为 0x12 0x34 0x 56 0x78
        public byte[] AscToBcd(String asc, int iLen)
        {
            int len = iLen;// = asc.Length;
            int mod = iLen % 2;
            if (mod != 0)
            {
                asc = "0" + asc;
                len = asc.Length;
            }

            byte[] abt = new byte[len];
            if (len >= 2)
            {
                len = len / 2;
            }

            byte[] bbt = new byte[len];
            abt = System.Text.Encoding.ASCII.GetBytes(asc);

            int j, k;
            for (int p = 0; p < asc.Length / 2; p++)
            {
                if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
                {
                    j = abt[2 * p] - '0';
                }
                else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
                {
                    j = abt[2 * p] - 'a' + 0x0a;
                }
                else
                {
                    j = abt[2 * p] - 'A' + 0x0a;
                }

                if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
                {
                    k = abt[2 * p + 1] - '0';
                }
                else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
                {
                    k = abt[2 * p + 1] - 'a' + 0x0a;
                }
                else
                {
                    k = abt[2 * p + 1] - 'A' + 0x0a;
                }

                int a = (j << 4) + k;
                byte b = (byte)a;
                bbt[p] = b;
            }

            return bbt;
        }
    }

}
