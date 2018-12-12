using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UBPayApp
{
    class PayInterface
    {
        //1.2下单
        public bool InterfaceAddOrder(string paychannel, string payment, string amount, string auth_code, string subject, string body, string merchant_id, string store_id, string operator_id, string device_id, string key, out string order_id, out Dictionary<string, string> result)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/channel/add_order";
            bool ret = false;
            
            //key = "bf0ac37accfb6ed19d4676a54d6aaaf0";

            result = new Dictionary<string, string>();
            order_id = string.Empty;

            //1 json
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("payment", payment);
            dic.Add("channel", paychannel);
            dic.Add("order_id", "");
            dic.Add("amount", amount);
            dic.Add("auth_code", auth_code);
            dic.Add("subject", subject);
            dic.Add("body", body);
            dic.Add("store_id", store_id);
            dic.Add("operator_id", operator_id);
            dic.Add("device_id", device_id);
            dic.Add("timestamp", GetTimeStamp());

            string jsonData = JsonConvert.SerializeObject(dic);
            LogManager.WriteLogTran(LogType.Message, "InterfaceAddOrder jsonData= ", jsonData.ToString());

            //2 encrypt
            string encryptData = AESEncrypt(jsonData, Var.gkey);

            string httppara = string.Format("merchant_id={0}&data={1}", merchant_id, encryptData);
            LogManager.WriteLogTran(LogType.Message, "InterfaceAddOrder httppara= ", httppara.ToString());

            //3 send
            string inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "InterfaceAddOrder Result= ", inResult.ToString());

            try
            {
                //4 get return json
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    return false;
                }

                //5 judge
                if (result_json["code"].ToString() == "1")
                {
                    inResult = result_json["msg"].ToString();
                    LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                    string tmpdata = result_json["data"].ToString();

                    string tmpJson = AESDecrypt(tmpdata, Var.gkey);

                    result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpJson);

                    result.TryGetValue("order_id", out order_id);                    

                    ret = true;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    ret = false;
                }
                else
                {
                    //log
                    ret = false;
                }
            }
            catch (Exception)
            {                
                ret = false;
            }
            return ret;
        }

        //1.3 订单状态查询 add 20181023
        public bool ApiOrderStatusQuery(string order_id, string merchant_id,out Dictionary<string,string>result)        
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/channel/query_order";
            bool ret = false;

            //string key = "bf0ac37accfb6ed19d4676a54d6aaaf0";

            result = new Dictionary<string, string>();

            //1 json
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("order_id", order_id);
            dic.Add("timestamp", GetTimeStamp());
            string jsonData = JsonConvert.SerializeObject(dic);
            LogManager.WriteLogTran(LogType.Message, "ApiOrderStatusQuery jsonData= ", jsonData.ToString());

            //2 encrypt
            string encryptData = AESEncrypt(jsonData, Var.gkey);

            string httppara = string.Format("merchant_id={0}&data={1}", merchant_id, encryptData);            
            LogManager.WriteLogTran(LogType.Message, "ApiOrderStatusQuery httppara= ", httppara.ToString());

            //3 send
            string inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "ApiOrderStatusQuery Result= ", inResult.ToString());

            try
            {
                //4 get return json
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    return false;
                }

                //5 judge
                if (result_json["code"].ToString() == "1")
                {
                    inResult = result_json["msg"].ToString();
                    LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                    string tmpdata = result_json["data"].ToString();

                    string tmpJson = AESDecrypt(tmpdata, Var.gkey);

                    result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpJson);

                    ret = true;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    result.Add("msg", "更新失败");
                    ret = false;
                }
                else
                {
                    //log
                    ret = false;
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        //1.6退款 add 20181010 
        public bool ApiAddRefund(string order_id, string refund_amount, string operator_id, string merchant_id, out Dictionary<string, string> result)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/channel/add_refund";
            bool ret = false;

            //string key = "bf0ac37accfb6ed19d4676a54d6aaaf0";
            result = new Dictionary<string, string>();

            //1 json
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("order_id", order_id);
            dic.Add("refund_amount", refund_amount);
            dic.Add("operator_id", operator_id);
            dic.Add("timestamp", GetTimeStamp());
            string jsonData = JsonConvert.SerializeObject(dic);
            LogManager.WriteLogTran(LogType.Message, "ApiAddRefund jsonData= ", jsonData.ToString());

            //2 encrypt
            string encryptData = AESEncrypt(jsonData, Var.gkey);

            string httppara = string.Format("merchant_id={0}&data={1}", merchant_id, encryptData);
            LogManager.WriteLogTran(LogType.Message, "ApiAddRefund httppara= ", httppara.ToString());

            //3 send
            string inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "ApiAddRefund Result= ", inResult.ToString());

            try
            {
                //4 get return json
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    return false;
                }

                //5 judge
                if (result_json["code"].ToString() == "1")
                {
                    inResult = result_json["msg"].ToString();
                    LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                    string tmpdata = result_json["data"].ToString();

                    string tmpJson = AESDecrypt(tmpdata, Var.gkey);

                    result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpJson);
                    ret = true;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    inResult = result_json["msg"].ToString();
                    result.Add("msg", "inResult");
                    ret = false;
                }
                else
                {
                    //log
                    ret = false;
                }
            }
            catch (Exception)
            {                
                ret = false;
            }
            return ret;
        }

        //1.7 退款状态查询 add 20181108
        public bool ApiRefundStatusQuery(string refund_order_id, string merchant_id, out Dictionary<string, string> result)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/channel/query_refund";
            bool ret = false;

            //string key = "bf0ac37accfb6ed19d4676a54d6aaaf0";

            result = new Dictionary<string, string>();

            //1 json
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("refund_order_id", refund_order_id);
            dic.Add("timestamp", GetTimeStamp());
            string jsonData = JsonConvert.SerializeObject(dic);
            LogManager.WriteLogTran(LogType.Message, "ApiRefundStatusQuery jsonData= ", jsonData.ToString());

            //2 encrypt
            string encryptData = AESEncrypt(jsonData, Var.gkey);

            string httppara = string.Format("merchant_id={0}&data={1}", merchant_id, encryptData);
            LogManager.WriteLogTran(LogType.Message, "ApiRefundStatusQuery httppara= ", httppara.ToString());

            //3 send
            string inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "ApiRefundStatusQuery Result= ", inResult.ToString());

            try
            {
                //4 get return json
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    return false;
                }

                //5 judge
                if (result_json["code"].ToString() == "1")
                {
                    inResult = result_json["msg"].ToString();
                    LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                    string tmpdata = result_json["data"].ToString();

                    string tmpJson = AESDecrypt(tmpdata, Var.gkey);

                    result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpJson);

                    ret = true;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    result.Add("msg", "更新失败");
                    ret = false;
                }
                else
                {
                    //log
                    ret = false;
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }



        public void test()
        {
            string str = AESEncrypt("123456", "6D6A39C7078F6783E561B0D1A9EB2E68");

            string str2 = AESDecrypt(str, "6D6A39C7078F6783E561B0D1A9EB2E68");
        }

        /*****************************************************************************************/
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        const string AES_IV = "1234567890000000";//16位    

        public static string AESEncrypt(String Data, String Key)
        {
            byte[] keyArray = UTF8Encoding.ASCII.GetBytes(Key);
            byte[] toEncryptArray = UTF8Encoding.ASCII.GetBytes(Data);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return BytesToHexString(resultArray).ToLower();
        }

        public static string AESEncrypt03(String Data, String Key)
        {
            byte[] keyArray = HexStringToBytes(Key);

            byte[] toEncryptArray = UTF8Encoding.ASCII.GetBytes(Data);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0,
                    toEncryptArray.Length);

            //0x11, 0x22, 0x33 -> "112233"
            return BytesToHexString(resultArray);
        }

        public static string AESEncrypt02(String Data, String Key)
        {
            MYLIB mylib = new MYLIB();

            // 256-AES key      
            //byte[] keyArray = new byte[Key.Length / 2];
            //mylib.GetHexFromString(Key, keyArray);

            byte[] keyArray = HexStringToBytes(Key);

            //byte[] toEncryptArray = Encoding.UTF8.GetBytes(Data);
            byte[] toEncryptArray = UTF8Encoding.ASCII.GetBytes(Data);

            RijndaelManaged rDel = new RijndaelManaged();
            //rDel.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0,
                    toEncryptArray.Length);

            //0x11, 0x22, 0x33 -> "112233"
            return BytesToHexString(resultArray);
        }


        /// <summary>
        /// AES解密(无向量)
        /// </summary>
        /// <param name="encryptedBytes">被加密的明文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(String Data, String Key)
        {
            MYLIB mylib = new MYLIB();
            Byte[] encryptedBytes = mylib.AscToBcd(Data, Data.Length);
            Byte[] bKey = new Byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(Key.PadRight(bKey.Length)), bKey, bKey.Length);

            MemoryStream mStream = new MemoryStream(encryptedBytes);
            //mStream.Write( encryptedBytes, 0, encryptedBytes.Length );
            //mStream.Seek( 0, SeekOrigin.Begin );
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.KeySize = 128;
            aes.Key = bKey;
            //aes.IV = _iV;
            CryptoStream cryptoStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            try
            {
                byte[] tmp = new byte[encryptedBytes.Length + 32];
                int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length + 32);
                byte[] ret = new byte[len];
                Array.Copy(tmp, 0, ret, 0, len);
                return Encoding.UTF8.GetString(ret);
            }
            finally
            {
                cryptoStream.Close();
                mStream.Close();
                aes.Clear();
            }
        }


        public static string AESDecrypt(String Data, String Key ,String Vector)
        {
            MYLIB mylib = new MYLIB();

            // 256-AES key      
            byte[] keyArray = new byte[Key.Length / 2];
            mylib.GetHexFromString(Key, keyArray);

            //"112233" -> 0x11, 0x22, 0x33
            byte[] toEncryptArray = new byte[Data.Length / 2];
            mylib.GetHexFromString(Data, toEncryptArray);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.IV = Encoding.UTF8.GetBytes(Vector.Substring(0, 16));
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0,
                    toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        public static string BytesToHexString(byte[] bytes)
        {
            StringBuilder returnStr = new StringBuilder();
            if (bytes != null || bytes.Length == 0)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr.Append(bytes[i].ToString("x2"));
                }
            }
            return returnStr.ToString();
        }

        public static byte[] HexStringToBytes(String hexString)
        {
            if (hexString == null || hexString.Equals(""))
            {
                return null;
            }
            int length = hexString.Length / 2;
            if (hexString.Length % 2 != 0)
            {
                return null;
            }
            byte[] d = new byte[length];
            for (int i = 0; i < length; i++)
            {
                d[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return d;

        }

            public static string HttpPost(string url, string data, Dictionary<string, string> headerDic = null)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "POST";
                wbRequest.ContentType = "application/x-www-form-urlencoded";
                wbRequest.ContentLength = Encoding.UTF8.GetByteCount(data);
                if (headerDic != null && headerDic.Count > 0)
                {
                    foreach (var item in headerDic)
                    {
                        wbRequest.Headers.Add(item.Key, item.Value);
                    }
                }
                using (Stream requestStream = wbRequest.GetRequestStream())
                {
                    using (StreamWriter swrite = new StreamWriter(requestStream))
                    {
                        swrite.Write(data);
                    }
                }
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sread = new StreamReader(responseStream))
                    {
                        result = sread.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static string HttpGet(string url)
        {
            string result = string.Empty;
            try
            {
                HttpWebRequest wbRequest = (HttpWebRequest)WebRequest.Create(url);
                wbRequest.Method = "GET";
                HttpWebResponse wbResponse = (HttpWebResponse)wbRequest.GetResponse();
                using (Stream responseStream = wbResponse.GetResponseStream())
                {
                    using (StreamReader sReader = new StreamReader(responseStream))
                    {
                        result = sReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
