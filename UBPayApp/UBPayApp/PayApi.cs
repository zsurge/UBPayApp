using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace UBPayApp
{
    class PayApi
    {
        //15．微信登录-轮询获取token
        public static int ApiQrCodeLogin(string guid, out string inResult)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/auth/token";
            string httppara = string.Format("guid={0}", guid);

            LogManager.WriteLogTran(LogType.Message, "QrCodeLogin Send = ", httppara.ToString());
            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            int ret = 0;

            try
            {
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    //log
                    inResult = "与平台通讯异常";
                    return 0;
                }

                if (result_json["code"].ToString() == "1")
                {
                    Var.ltoken = result_json["data"]["token"].ToString();
                    inResult = result_json["msg"].ToString();

                    ret = 1;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    inResult = result_json["msg"].ToString();
                    ret = 0;
                }
                else
                {
                    inResult = result_json["msg"].ToString();
                    ret = 0;
                }

            }
            catch (Exception)
            {
                inResult = "与平台通讯异常";
                ret = 0;
            }
            return ret;
        }

        // 14．微信登录-获取二维码
        // http://cashier.cdsoftware.cn/index.php/api/auth/qrcode
        public static bool ApiGetQrCode(string User, out string inResult, out string guid, out string url)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/auth/qrcode";
            string httppara = string.Format("type={0}", User);
            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "GetQrCode Result= ", inResult.ToString());
            bool ret = false;
            url = "";
            guid = "";
            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;

            if (result_json == null)
            {
                inResult = "与平台通讯异常";
                ret = false;
                return ret;
            }

            if (result_json.Count == 0)
            {
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                guid = result_json["data"]["guid"].ToString();
                LogManager.WriteLogTran(LogType.Message, "guid= ", guid.ToString());

                url = result_json["data"]["url"].ToString();
                LogManager.WriteLogTran(LogType.Message, "url= ", url.ToString());
                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                ret = false;
            }
            else
            {
                //log
                inResult = "与平台通讯异常";
                ret = false;
            }

            return ret;
        }

        //01登陆
        public static bool ApiLogin(string inUsername, string inPassword, out string inResult)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/site/login";
            string httppara = string.Format("username={0}&password={1}", inUsername, inPassword);

            LogManager.WriteLogTran(LogType.Message, "===================用户登陆===================", "Start");
            LogManager.WriteLogTran(LogType.Message, "Login Send = ", httppara.ToString());
            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            try
            {
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                if (result_json.Count == 0)
                {
                    //log
                    return false;
                }
                if (result_json["code"].ToString() == "1")
                {
                    Var.ltoken = result_json["data"]["token"].ToString();
                    inResult = result_json["msg"].ToString();

                    ret = true;
                }
                else if (result_json["code"].ToString() == "0")
                {
                    inResult = result_json["msg"].ToString();
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
                inResult = "与平台通讯异常";
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================用户登陆===================", "End");
            return ret;
        }

        //3用户信息
        public static bool ApiGetUserInfo(string inToken, out string inResult, out Var.User_Info userinfo, out Var.agent_Info agentinfo, out Var.merchant_Info merchantinfo, out Var.store_Info storeinfo)
        {
            userinfo = new Var.User_Info();
            agentinfo = new Var.agent_Info();
            merchantinfo = new Var.merchant_Info();
            storeinfo = new Var.store_Info();

            LogManager.WriteLogTran(LogType.Message, "===================用户信息===================", "Start");
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/info";

            string httppara = string.Format("key={0}", inToken);
            LogManager.WriteLogTran(LogType.Message, "GetUserInfo Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());
                string agent = result_json["data"]["agent"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agent = ", agent.ToString());
                string merchant = result_json["data"]["merchant"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchant = ", merchant.ToString());
                string store = result_json["data"]["store"].ToString();
                LogManager.WriteLogTran(LogType.Message, "store = ", store.ToString());

                userinfo.id = result_json["data"]["id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.id = ", userinfo.id.ToString());
                userinfo.agent_id = result_json["data"]["agent_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.agent_id = ", userinfo.agent_id.ToString());
                userinfo.merchant_id = result_json["data"]["merchant_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.merchant_id = ", userinfo.merchant_id.ToString());
                userinfo.store_id = result_json["data"]["store_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.store_id = ", userinfo.store_id.ToString());
                userinfo.username = result_json["data"]["username"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.username = ", userinfo.username.ToString());
                userinfo.phone = result_json["data"]["phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.phone = ", userinfo.phone.ToString());
                userinfo.open_id = result_json["data"]["open_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.open_id = ", userinfo.open_id.ToString());
                userinfo.type = result_json["data"]["type"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.type = ", userinfo.type.ToString());
                userinfo.status = result_json["data"]["status"].ToString();
                LogManager.WriteLogTran(LogType.Message, "userinfo.status = ", userinfo.status.ToString());

                agentinfo.id = result_json["data"]["agent"]["id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.id = ", agentinfo.id.ToString());
                agentinfo.pid = result_json["data"]["agent"]["pid"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.pid = ", agentinfo.pid.ToString());
                agentinfo.name = result_json["data"]["agent"]["name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.name = ", agentinfo.name.ToString());
                agentinfo.number = result_json["data"]["agent"]["number"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.number = ", agentinfo.number.ToString());
                agentinfo.address = result_json["data"]["agent"]["address"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.address = ", agentinfo.address.ToString());
                agentinfo.principal_name = result_json["data"]["agent"]["principal_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.principal_name = ", agentinfo.principal_name.ToString());
                agentinfo.principal_phone = result_json["data"]["agent"]["principal_phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.principal_phone = ", agentinfo.principal_phone.ToString());
                agentinfo.contact_name = result_json["data"]["agent"]["contact_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.contact_name = ", agentinfo.contact_name.ToString());
                agentinfo.contact_phone = result_json["data"]["agent"]["contact_phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.contact_phone = ", agentinfo.contact_phone.ToString());
                agentinfo.contact_email = result_json["data"]["agent"]["contact_email"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.contact_email = ", agentinfo.contact_email.ToString());
                agentinfo.status = result_json["data"]["agent"]["status"].ToString();
                LogManager.WriteLogTran(LogType.Message, "agentinfo.status = ", agentinfo.status.ToString());

                merchantinfo.id = result_json["data"]["merchant"]["id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.id = ", merchantinfo.id.ToString());
                merchantinfo.agent_id = result_json["data"]["merchant"]["agent_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.agent_id = ", merchantinfo.agent_id.ToString());
                merchantinfo.name = result_json["data"]["merchant"]["name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.name = ", merchantinfo.name.ToString());
                merchantinfo.short_name = result_json["data"]["merchant"]["short_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.short_name = ", merchantinfo.short_name.ToString());
                merchantinfo.number = result_json["data"]["merchant"]["number"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.number = ", merchantinfo.number.ToString());
                merchantinfo.principal_name = result_json["data"]["merchant"]["principal_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.principal_name = ", merchantinfo.principal_name.ToString());
                merchantinfo.principal_phone = result_json["data"]["merchant"]["principal_phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.principal_phone = ", merchantinfo.principal_phone.ToString());
                merchantinfo.contact_name = result_json["data"]["merchant"]["contact_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.contact_name = ", merchantinfo.contact_name.ToString());
                merchantinfo.contact_phone = result_json["data"]["merchant"]["contact_phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.contact_phone = ", merchantinfo.contact_phone.ToString());
                merchantinfo.contact_email = result_json["data"]["merchant"]["contact_email"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.contact_email = ", merchantinfo.contact_email.ToString());
                merchantinfo.address = result_json["data"]["merchant"]["address"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.address = ", merchantinfo.address.ToString());
                merchantinfo.status = result_json["data"]["merchant"]["status"].ToString();
                LogManager.WriteLogTran(LogType.Message, "merchantinfo.status = ", merchantinfo.status.ToString());

                storeinfo.id = result_json["data"]["store"]["id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.id = ", storeinfo.id.ToString());
                storeinfo.merchant_id = result_json["data"]["store"]["merchant_id"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.merchant_id = ", storeinfo.merchant_id.ToString());
                storeinfo.name = result_json["data"]["store"]["name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.name = ", storeinfo.name.ToString());
                storeinfo.number = result_json["data"]["store"]["number"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.number = ", storeinfo.number.ToString());
                storeinfo.principal_name = result_json["data"]["store"]["principal_name"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.principal_name = ", storeinfo.principal_name.ToString());
                storeinfo.principal_phone = result_json["data"]["store"]["principal_phone"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.principal_phone = ", storeinfo.principal_phone.ToString());
                storeinfo.address = result_json["data"]["store"]["address"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.address = ", storeinfo.address.ToString());
                storeinfo.status = result_json["data"]["store"]["status"].ToString();
                LogManager.WriteLogTran(LogType.Message, "storeinfo.status = ", storeinfo.status.ToString());
                

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================用户信息===================", "End");
            return ret;
        }

        //4门店列表
        public static bool ApiGetStoreList(string inToken, out string inResult, out Var.StoreList_Info[] storelist, out int Count)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/store_list";
            storelist = new Var.StoreList_Info[128];
            Count = 0;

            LogManager.WriteLogTran(LogType.Message, "===================门店列表===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "GetStoreList Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());

            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                //LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                string list = result_json["data"]["list"].ToString();
                LogManager.WriteLogTran(LogType.Message, "list = ", list.ToString());

                JArray ja = (JArray)JsonConvert.DeserializeObject(list);

                try
                {
                    for (int i = 0; i < 128; i++)       // 处理2个界面应该足够了吧
                    {
                        storelist[i].id=ja[i]["id"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].id = ", storelist[i].id.ToString());

                        storelist[i].merchant_id=ja[i]["merchant_id"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].merchant_id = ", storelist[i].merchant_id.ToString());

                        storelist[i].name = ja[i]["name"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].name = ", storelist[i].name.ToString());

                        storelist[i].number = ja[i]["number"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].number = ", storelist[i].number.ToString());

                        storelist[i].principal_name = ja[i]["principal_name"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].principal_name = ", storelist[i].principal_name.ToString());

                        storelist[i].principal_phone = ja[i]["principal_phone"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].principal_phone = ", storelist[i].principal_phone.ToString());

                        storelist[i].address = ja[i]["address"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].address = ", storelist[i].address.ToString());

                        storelist[i].status = ja[i]["status"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "storelist[" + (i + 1).ToString() + "].status = ", storelist[i].status.ToString());
                        Count++;
                    }
                }
                catch (Exception)
                {

                }
                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================门店列表===================", "End");
            return ret;
        }

        //5店员列表
        public static bool ApiGetUserList(string inStoreId, string inToken, out string inResult, out Var.UserList_Info[] userlist, out int Count)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/user_list";
            userlist = new Var.UserList_Info[128];
            Count = 0;
            LogManager.WriteLogTran(LogType.Message, "===================店员列表===================", "Start");
            string httppara = string.Format("key={0}&store_id={1}", inToken, inStoreId);

            LogManager.WriteLogTran(LogType.Message, "GetUserList Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                //LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                string list = result_json["data"]["list"].ToString();
                LogManager.WriteLogTran(LogType.Message, "list = ", list.ToString());

                JArray ja = (JArray)JsonConvert.DeserializeObject(list);

                try
                {
                    for (int i = 0; i < 128; i++)       // 处理2个界面应该足够了吧
                    {
                        userlist[i].id = ja[i]["id"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "userlist[" + (i + 1).ToString() + "].id = ", userlist[i].id.ToString());

                        userlist[i].username = ja[i]["username"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "userlist[" + (i + 1).ToString() + "].username = ", userlist[i].username.ToString());

                        userlist[i].phone = ja[i]["phone"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "userlist[" + (i + 1).ToString() + "].phone = ", userlist[i].phone.ToString());
                        Count++;
                    }
                }
                catch (Exception)
                {

                }
                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================店员列表===================", "End");
            return ret;
        }

        

        //6设备列表
        public static bool ApiGetDeviceList(string inStoreId, string inToken, out string inResult, out Var.DeviceList_Info[] devicelist, out int Count)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/device_list";
            devicelist = new Var.DeviceList_Info[128];
            Count = 0;

            LogManager.WriteLogTran(LogType.Message, "===================设备列表===================", "Start");
            string httppara = string.Format("key={0}&store_id={1}", inToken, inStoreId);

            LogManager.WriteLogTran(LogType.Message, "GetDeviceList Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                //LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                string list = result_json["data"]["list"].ToString();
                LogManager.WriteLogTran(LogType.Message, "list = ", list.ToString());

                JArray ja = (JArray)JsonConvert.DeserializeObject(list);

                try
                {
                    for (int i = 0; i < 128; i++)       // 
                    {
                        devicelist[i].id = ja[i]["id"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "devicelist[" + (i + 1).ToString() + "].id = ", devicelist[i].id.ToString());

                        devicelist[i].name = ja[i]["name"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "devicelist[" + (i + 1).ToString() + "].name = ", devicelist[i].name.ToString());

                        devicelist[i].number = ja[i]["number"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "devicelist[" + (i + 1).ToString() + "].number = ", devicelist[i].number.ToString());

                        devicelist[i].type = ja[i]["type"].ToString();
                        LogManager.WriteLogTran(LogType.Message, "devicelist[" + (i + 1).ToString() + "].type = ", devicelist[i].type.ToString());
                        Count++;
                    }
                }
                catch (Exception)
                {

                }
                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = null;
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================设备列表===================", "End");
            return ret;
        }       

        //11 支付方式列表 add 2018.10.21 zd
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken"></param>
        /// <param name="inResult"></param>
        /// <param name="paymentList"></param>
        /// <returns></returns>
        public static bool ApiGet_all_paymentChannel(string inToken, out string inResult, out Dictionary<string, string> paymentList)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/get_all_channel";


            LogManager.WriteLogTran(LogType.Message, "===================交易方式列表===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "Get_all_payment_channel = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;

            paymentList = new Dictionary<string, string>();

            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }   

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());


                paymentList = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

                //paymentList[0].payment_channel = result_json["data"];
                //LogManager.WriteLogTran(LogType.Message, "paymentList[0].payment_channel = ", paymentList[0].payment_channel.ToString());
                //paymentList[1].payment_channel = result_json["data"]["barcode"].ToString();
                //LogManager.WriteLogTran(LogType.Message, "paymentList[1].payment_channel = ", paymentList[1].payment_channel.ToString());
                //Count = 2;

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================交易方式列表===================", "End");
            return ret;
        }

        //12．交易类型列表
        public static bool ApiGet_all_paymentList(string inToken, out string inResult, out Dictionary<string, string> paymentList)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/get_all_payment";
            paymentList = new Dictionary<string, string>();

            LogManager.WriteLogTran(LogType.Message, "===================交易类型列表===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "Get_all_paymentList Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                paymentList = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================交易类型列表===================", "End");
            return ret;
        }

        //08订单统计列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken">key</param>
        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// <param name="time_start">2018-06-12 00:00:00 开始时间
        /// <param name="time_end">2018-07-23 00:00:00 结束时间
        /// <param name="channel">支付方式 使用支付方式接口获取</param>
        /// <param name="payment">交易类型 使用交易类型接口获取</param>
        /// <param name="order_id">订单号</param>
        /// <param name="store_id">门店id 使用门店列表接口获取</param>
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="device_id">设备id 使用设备列表接口获取</param>
        /// <param name="merchant_id">商户id</param>
        /// <param name="status">通过订单状态接口获取</param>
        /// <param name="export">为1而全部记录</param>
        /// <param name="inResult"></param>
        /// <param name="paymentList"></param>
        /// <param name="Count"></param>
        /// /// <param name="Order_QueryList_total">总的数据条目</param>   //add for 2018.10.5 
        /// <returns></returns>
        public static bool ApiGetOrder_List(string inToken, int iPage, int Current, int time_type, string time_start, string time_end, string channel, string payment, string order_id, string store_id, string operator_id, string device_id, string merchant_id, string status, int export,out string inResult, out Var.all_tran_list[] paymentList, out int Count,out int Order_QueryList_total)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/order_list";            
            paymentList = new Var.all_tran_list[1024*1024];
            Count = 0;
            Order_QueryList_total = 0;
            LogManager.WriteLogTran(LogType.Message, "===================交易查询===================", "Start");
            string httppara = string.Format("key={0}&page={1}&current={2}&time_type={3}&time_start={4}&time_end={5}&channel={6}&payment={7}&order_id={8}&store_id={9}&operator_id={10}&device_id={11}&merchant_id={12}&status={13}&export={14}", inToken, iPage, Current, time_type, time_start, time_end,channel, payment, order_id, store_id, operator_id, device_id, merchant_id, status, export);

            //string httppara = string.Format("key={0}&page={1}&current={2}&&merchant_id={3}&export={4}", inToken, iPage, Current,  merchant_id, export);

            LogManager.WriteLogTran(LogType.Message, "GetOrder_List Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            
            try
            {
                Order_QueryList_total = Convert.ToInt32(result_json["data"]["total"].ToString());
            }
            catch
            {

            }


            if (result_json["code"].ToString() == "1")
            {
                //MessageBox.Show(result_json["data"]["list"].ToString());

                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                Count = result_json["data"]["list"].Count();
                for(int i = 0; i<Count; i++)
                {
                    JArray jlist = result_json["data"].Value<JArray>("list");
                    JObject jcontent = JObject.Parse(jlist[i].ToString());

                    paymentList[i].id = jcontent.Value<string>("id");
                    paymentList[i].order_id = jcontent.Value<string>("order_id");
                    paymentList[i].out_order_id = jcontent.Value<string>("out_order_id");
                    paymentList[i].out_username = jcontent.Value<string>("out_username");
                    paymentList[i].out_user_id = jcontent.Value<string>("out_user_id");
                    paymentList[i].agent_id = jcontent.Value<string>("agent_id");
                    paymentList[i].merchant_id = jcontent.Value<string>("merchant_id");
                    paymentList[i].channel = jcontent.Value<string>("channel");
                    paymentList[i].payment = jcontent.Value<string>("payment");
                    paymentList[i].store_id = jcontent.Value<string>("store_id");
                    paymentList[i].store_name = jcontent.Value<string>("store_name");
                    paymentList[i].operator_id = jcontent.Value<string>("operator_id");
                    paymentList[i].operator_name = jcontent.Value<string>("operator_name");
                    paymentList[i].device_id = jcontent.Value<string>("device_id");
                    paymentList[i].device_name = jcontent.Value<string>("device_name");
                    paymentList[i].amount = jcontent.Value<string>("amount");
                    paymentList[i].actual_amount = jcontent.Value<string>("actual_amount");
                    paymentList[i].refund_amount = jcontent.Value<string>("refund_amount");
                    paymentList[i].body = jcontent.Value<string>("body");
                    paymentList[i].subject = jcontent.Value<string>("subject");
                    paymentList[i].time_create = jcontent.Value<string>("time_create");
                    paymentList[i].time_update = jcontent.Value<string>("time_update");
                    paymentList[i].status = jcontent.Value<string>("status");
                    paymentList[i].remark = jcontent.Value<string>("remark");
                    paymentList[i]._status = jcontent.Value<string>("_status");
                    paymentList[i].payment_name = jcontent.Value<string>("payment_name");
                    paymentList[i].channel_name = jcontent.Value<string>("channel_name");
                }

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                MessageBox.Show(result_json["msg"].ToString(), "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================交易查询===================", "End");
            return ret;
        }


        //09．订单汇总
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken">key</param>
        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// <param name="time_start">2018-06-12 00:00:00 开始时间
        /// <param name="time_end">2018-07-23 00:00:00 结束时间
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="merchant_id">商户id</param>
        /// <param name="export">为1而全部记录</param>
        /// <param name="inResult"></param>
        /// <param name="paymentList"></param>
        /// <param name="Count"></param>
        /// <param name="Order_SumList_total">总的数据条目</param>   //add for 2018.10.5 
        /// <returns></returns>
        public static bool ApiGetOrder_SumList(string inToken, int iPage, int Current, int time_type, string time_start, string time_end, string operator_id, string merchant_id, int export,out string inResult, out Var.all_tran_sum[] paymentList, out int Count,out int Order_SumList_total)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/order_count_list";
            paymentList = new Var.all_tran_sum[1024];
            Count = 0;
            Order_SumList_total = 0;

            LogManager.WriteLogTran(LogType.Message, "===================订单汇总===================", "Start");
            string httppara = string.Format("key={0}&page={1}&current={2}&time_type={3}&time_start={4}&time_end={5}&operator_id={6}&merchant_id={7}", inToken, iPage, Current, time_type, time_start, time_end,operator_id, merchant_id);
            //string httppara = string.Format("key={0}&page={1}&current={2}&operator_id={3}&merchant_id={4}&export={5}", inToken, iPage, Current, operator_id, merchant_id, export);

            LogManager.WriteLogTran(LogType.Message, "GetOrder_List Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            try
            {
                Order_SumList_total = Convert.ToInt32(result_json["data"]["total"].ToString());
            }
            catch
            {

            }

            

            if (result_json["code"].ToString() == "1")
            {
                //MessageBox.Show(result_json["data"]["list"].ToString());

                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                Count = result_json["data"]["list"].Count();
                for (int i = 0; i < Count; i++)
                {
                    JArray jlist = result_json["data"].Value<JArray>("list");
                    JObject jcontent = JObject.Parse(jlist[i].ToString());

                    paymentList[i].id = jcontent.Value<string>("id");
                    paymentList[i].agent_id = jcontent.Value<string>("agent_id");
                    paymentList[i].merchant_id = jcontent.Value<string>("merchant_id");
                    paymentList[i].operator_id = jcontent.Value<string>("operator_id");
                    paymentList[i].time_create = jcontent.Value<string>("time_create");
                    paymentList[i].date = jcontent.Value<string>("date");
                    paymentList[i].count_weixin = jcontent.Value<string>("count_weixin");
                    paymentList[i].sum_weixin = jcontent.Value<string>("sum_weixin");
                    paymentList[i].count_alipay = jcontent.Value<string>("count_alipay");
                    paymentList[i].sum_alipay = jcontent.Value<string>("sum_alipay");
                    paymentList[i].count_unionpay = jcontent.Value<string>("count_unionpay");
                    paymentList[i].sum_unionpay = jcontent.Value<string>("sum_unionpay");
                    paymentList[i].count_xt = jcontent.Value<string>("count_xt");
                    paymentList[i].sum_xt = jcontent.Value<string>("sum_xt");
                    paymentList[i].count_xz_weixin = jcontent.Value<string>("count_xz_weixin");
                    paymentList[i].sum_xz_weixin = jcontent.Value<string>("sum_xz_weixin");
                    paymentList[i].count_xz_alipay = jcontent.Value<string>("count_xz_alipay");
                    paymentList[i].sum_xz_alipay = jcontent.Value<string>("sum_xz_alipay");
                    paymentList[i].count_sf_weixin = jcontent.Value<string>("count_sf_weixin");
                    paymentList[i].sum_sf_weixin = jcontent.Value<string>("sum_sf_weixin");
                    paymentList[i].count_sf_alipay = jcontent.Value<string>("count_sf_alipay");
                    paymentList[i].sum_sf_alipay = jcontent.Value<string>("sum_sf_alipay");
                    paymentList[i].count_all = jcontent.Value<string>("count_all");
                    paymentList[i].sum_all = jcontent.Value<string>("sum_all");
                }

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                MessageBox.Show(result_json["msg"].ToString(), "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }
            LogManager.WriteLogTran(LogType.Message, "===================订单汇总===================", "End");
            return ret;
        }

        //13订单状态查询
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken"></param>
        /// <param name="inResult"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool ApiGet_all_OrderStatus(string inToken,out string inResult, out Dictionary<string, string> result)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/get_all_order_status";

            result = new Dictionary<string, string>();

            LogManager.WriteLogTran(LogType.Message, "===================订单状态===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "ApiGet_all_OrderStatus Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            //5 judge
            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                string tmpdata = result_json["data"].ToString();          

                result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpdata);

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                result.Add("msg", "无权限");
                ret = false;
            }
            else
            {
                //log
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================订单状态===================", "End");
            return ret;
        }

        //14．退款状态
        public static bool ApiGet_all_RefundStatus(string inToken, out string inResult, out Dictionary<string, string> result)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/get_all_refund_status";

            result = new Dictionary<string, string>();

            LogManager.WriteLogTran(LogType.Message, "===================退款状态===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "ApiGet_all_RefundStatus Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            //5 judge
            if (result_json["code"].ToString() == "1")
            {
                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

                string tmpdata = result_json["data"].ToString();

                result = JsonConvert.DeserializeObject<Dictionary<string, string>>(tmpdata);

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                result.Add("msg", "无权限");
                ret = false;
            }
            else
            {
                //log
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================退款状态===================", "End");
            return ret;
        }

        //06 退款单查询        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken">key</param>
        /// <param name="iPage">每页多少条</param>
        /// <param name="Current">当前第几页</param>
        /// <param name="time_type">时间类型1当天 2昨天 3本周 4本月 为空则按照时间段查询</param>
        /// <param name="time_start">2018-06-12 00:00:00 开始时间
        /// <param name="time_end">2018-07-23 00:00:00 结束时间
        /// <param name="channel">支付方式 使用支付方式接口获取</param>
        /// <param name="payment">交易类型 使用交易类型接口获取</param>
        /// <param name="order_id">订单号</param>
        /// <param name="refund_order_id">退款单号</param>
        /// <param name="store_id">门店id 使用门店列表接口获取</param>
        /// <param name="operator_id">店员id 使用操作员列表接口获取</param>
        /// <param name="device_id">设备id 使用设备列表接口获取</param>
        /// <param name="merchant_id">商户id</param>
        /// <param name="status">通过订单状态接口获取</param>
        /// <param name="export">为1而全部记录</param>
        /// <param name="inResult"></param>
        /// <param name="paymentList"></param>
        /// <param name="Count"></param>
        /// <param name="Refund_QueryList_total">总的数据条目</param>   //add for 2018.10.5 
        /// <returns></returns>
        public static bool ApiGetRefund_QueryList(string inToken, int iPage, int Current, int time_type, string time_start, string time_end, string channel, string payment, string order_id,string refund_order_id, string store_id, string operator_id, string device_id, string merchant_id, string status,int export, out string inResult, out Var.all_tran_refund[] paymentList, out int Count, out int Refund_QueryList_total)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/refund_list";
            paymentList = new Var.all_tran_refund[1024*1024];
            Count = 0;
            Refund_QueryList_total = 0;      
                
            Count = 0;
            Refund_QueryList_total = 0;
            LogManager.WriteLogTran(LogType.Message, "===================退款查询===================", "Start");
            string httppara = string.Format("key={0}&page={1}&current={2}&time_type={3}&time_start={4}&time_end={5}&channel={6}&payment={7}&order_id={8}&refund_order_id={9}&store_id={10}&operator_id={11}&device_id={12}&merchant_id={13}&status={14}&export={15}", inToken, iPage, Current, time_type, time_start, time_end,channel, payment, order_id, refund_order_id, store_id, operator_id, device_id, merchant_id, status, export);
            //string httppara = string.Format("key={0}&page={1}&current={2}&merchant_id={3}&export={4}", inToken, iPage, Current, merchant_id,export);

            LogManager.WriteLogTran(LogType.Message, "GetRefund_QueryList Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }

            try
            {
                Refund_QueryList_total = Convert.ToInt32(result_json["data"]["total"].ToString());
            }
            catch
            {

            }

            if (result_json["code"].ToString() == "1")
            {
                //MessageBox.Show(result_json["data"]["list"].ToString());

                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                Count = result_json["data"]["list"].Count();
                for (int i = 0; i < Count; i++)
                {
                    JArray jlist = result_json["data"].Value<JArray>("list");
                    JObject jcontent = JObject.Parse(jlist[i].ToString());

                    paymentList[i].id = jcontent.Value<string>("id");
                    paymentList[i].order_id = jcontent.Value<string>("order_id");
                    paymentList[i].refund_order_id = jcontent.Value<string>("refund_order_id");
                    paymentList[i].refund_amount = jcontent.Value<string>("refund_amount");
                    paymentList[i].operator_id = jcontent.Value<string>("operator_id");
                    paymentList[i].operator_name = jcontent.Value<string>("operator_name");
                    paymentList[i].time_create = jcontent.Value<string>("time_create");
                    paymentList[i].time_update = jcontent.Value<string>("time_update");
                    paymentList[i].status = jcontent.Value<string>("status");
                    paymentList[i].reason = jcontent.Value<string>("reason");
                    paymentList[i].remark = jcontent.Value<string>("id");
                    paymentList[i].out_order_id = jcontent.Value<string>("out_order_id");
                    paymentList[i].out_user_id = jcontent.Value<string>("out_user_id");
                    paymentList[i].agent_id = jcontent.Value<string>("agent_id");
                    paymentList[i].amount = jcontent.Value<string>("amount");
                    paymentList[i].merchant_id = jcontent.Value<string>("merchant_id");
                    paymentList[i].channel = jcontent.Value<string>("channel");
                    paymentList[i].payment = jcontent.Value<string>("payment");
                    paymentList[i].store_id = jcontent.Value<string>("store_id");
                    paymentList[i].store_name = jcontent.Value<string>("store_name");
                    paymentList[i].device_id = jcontent.Value<string>("device_id");
                    paymentList[i].device_name = jcontent.Value<string>("device_name");
                    paymentList[i].subject = jcontent.Value<string>("subject");
                    paymentList[i].body = jcontent.Value<string>("body");
                    paymentList[i]._status = jcontent.Value<string>("_status");
                    paymentList[i].payment_name = jcontent.Value<string>("payment_name");
                    paymentList[i].channel_name = jcontent.Value<string>("channel_name");
                }

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                MessageBox.Show(result_json["msg"].ToString(), "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================退款查询===================", "End");
            return ret;
        }

        //1.9操作员统计
      
        /// <summary>
        /// 获取操作统计列表
        /// </summary>
        /// <param name="inToken"></param>
        /// <param name="operator_id"></param>
        /// <param name="device_id"></param>
        /// <param name="time_start"></param>
        /// <param name="time_end"></param>
        /// <param name="paymentList"></param>
        /// <returns></returns>
        public static bool ApiGetOrderCount(string inToken, string operator_id, string device_id, string time_start, string time_end, out string inResult, out Var.all_Order_Count paymentList)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/order_count";

            paymentList = new Var.all_Order_Count();
            LogManager.WriteLogTran(LogType.Message, "===================操作员订单查询===================", "Start");
            string httppara = string.Format("key={0}&operator_id={1}&device_id={2}&time_start={3}&time_end={4}", inToken, operator_id, device_id, time_start, time_end);

            LogManager.WriteLogTran(LogType.Message, "GetOrderCount Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            bool ret = false;

            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }


            if (result_json["code"].ToString() == "1")
            {
                //MessageBox.Show(result_json["data"]["list"].ToString());

                inResult = result_json["msg"].ToString();

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());               
                

                paymentList.weixin.count = result_json["data"]["weixin"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "weixin.count = ", paymentList.weixin.count);
                paymentList.weixin.count_refund = result_json["data"]["weixin"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "weixin.count_refund = ", paymentList.weixin.count_refund);
                paymentList.weixin.sum_amount = result_json["data"]["weixin"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "weixin.sum_amount = ", paymentList.weixin.sum_amount);
                paymentList.weixin.sum_actual_amount = result_json["data"]["weixin"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "weixin.sum_actual_amount = ", paymentList.weixin.sum_actual_amount);
                paymentList.weixin.sum_refund_amount = result_json["data"]["weixin"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "weixin.sum_refund_amount = ", paymentList.weixin.sum_refund_amount);

                paymentList.alipay.count = result_json["data"]["alipay"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "alipay.count = ", paymentList.alipay.count);
                paymentList.alipay.count_refund = result_json["data"]["alipay"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "alipay.count_refund = ", paymentList.alipay.count_refund);
                paymentList.alipay.sum_amount = result_json["data"]["alipay"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "alipay.sum_amount = ", paymentList.alipay.sum_amount);
                paymentList.alipay.sum_actual_amount = result_json["data"]["alipay"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "alipay.sum_actual_amount = ", paymentList.alipay.sum_actual_amount);
                paymentList.alipay.sum_refund_amount = result_json["data"]["alipay"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "alipay.sum_refund_amount = ", paymentList.alipay.sum_refund_amount);

                paymentList.unionpay.count = result_json["data"]["unionpay"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "unionpay.count = ", paymentList.unionpay.count);
                paymentList.unionpay.count_refund = result_json["data"]["unionpay"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "unionpay.count_refund = ", paymentList.unionpay.count_refund);
                paymentList.unionpay.sum_amount = result_json["data"]["unionpay"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "unionpay.sum_amount = ", paymentList.unionpay.sum_amount);
                paymentList.unionpay.sum_actual_amount = result_json["data"]["unionpay"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "unionpay.sum_actual_amount = ", paymentList.unionpay.sum_actual_amount);
                paymentList.unionpay.sum_refund_amount = result_json["data"]["unionpay"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "unionpay.sum_refund_amount = ", paymentList.unionpay.sum_refund_amount);

                paymentList.xt.count = result_json["data"]["xt"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xt.count = ", paymentList.xt.count);
                paymentList.xt.count_refund = result_json["data"]["xt"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xt.count_refund = ", paymentList.xt.count_refund);
                paymentList.xt.sum_amount = result_json["data"]["xt"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xt.sum_amount = ", paymentList.xt.sum_amount);
                paymentList.xt.sum_actual_amount = result_json["data"]["xt"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xt.sum_actual_amount = ", paymentList.xt.sum_actual_amount);
                paymentList.xt.sum_refund_amount = result_json["data"]["xt"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xt.sum_refund_amount = ", paymentList.xt.sum_refund_amount);


                paymentList.xz_weixin.count = result_json["data"]["xz_weixin"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_weixin.count = ", paymentList.xz_weixin.count);
                paymentList.xz_weixin.count_refund = result_json["data"]["xz_weixin"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_weixin.count_refund = ", paymentList.xz_weixin.count_refund);
                paymentList.xz_weixin.sum_amount = result_json["data"]["xz_weixin"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_weixin.sum_amount = ", paymentList.xz_weixin.sum_amount);
                paymentList.xz_weixin.sum_actual_amount = result_json["data"]["xz_weixin"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_weixin.sum_actual_amount = ", paymentList.xz_weixin.sum_actual_amount);
                paymentList.xz_weixin.sum_refund_amount = result_json["data"]["xz_weixin"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_weixin.sum_refund_amount = ", paymentList.xz_weixin.sum_refund_amount);

                paymentList.xz_alipay.count = result_json["data"]["xz_alipay"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_alipay.count = ", paymentList.xz_alipay.count);
                paymentList.xz_alipay.count_refund = result_json["data"]["xz_alipay"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_alipay.count_refund = ", paymentList.xz_alipay.count_refund);
                paymentList.xz_alipay.sum_amount = result_json["data"]["xz_alipay"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_alipay.sum_amount = ", paymentList.xz_alipay.sum_amount);
                paymentList.xz_alipay.sum_actual_amount = result_json["data"]["xz_alipay"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_alipay.sum_actual_amount = ", paymentList.xz_alipay.sum_actual_amount);
                paymentList.xz_alipay.sum_refund_amount = result_json["data"]["xz_alipay"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "xz_alipay.sum_refund_amount = ", paymentList.xz_alipay.sum_refund_amount);

                paymentList.sf_weixin.count = result_json["data"]["sf_weixin"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_weixin.count = ", paymentList.sf_weixin.count);
                paymentList.sf_weixin.count_refund = result_json["data"]["sf_weixin"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_weixin.count_refund = ", paymentList.sf_weixin.count_refund);
                paymentList.sf_weixin.sum_amount = result_json["data"]["sf_weixin"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_weixin.sum_amount = ", paymentList.sf_weixin.sum_amount);
                paymentList.sf_weixin.sum_actual_amount = result_json["data"]["sf_weixin"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_weixin.sum_actual_amount = ", paymentList.sf_weixin.sum_actual_amount);
                paymentList.sf_weixin.sum_refund_amount = result_json["data"]["sf_weixin"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_weixin.sum_refund_amount = ", paymentList.sf_weixin.sum_refund_amount);

                paymentList.sf_alipay.count = result_json["data"]["sf_alipay"]["count"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_alipay.count = ", paymentList.sf_alipay.count);
                paymentList.sf_alipay.count_refund = result_json["data"]["sf_alipay"]["count_refund"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_alipay.count_refund = ", paymentList.sf_alipay.count_refund);
                paymentList.sf_alipay.sum_amount = result_json["data"]["sf_alipay"]["sum_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_alipay.sum_amount = ", paymentList.sf_alipay.sum_amount);
                paymentList.sf_alipay.sum_actual_amount = result_json["data"]["sf_alipay"]["sum_actual_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_alipay.sum_actual_amount = ", paymentList.sf_alipay.sum_actual_amount);
                paymentList.sf_alipay.sum_refund_amount = result_json["data"]["sf_alipay"]["sum_refund_amount"].ToString();
                LogManager.WriteLogTran(LogType.Message, "sf_alipay.sum_refund_amount = ", paymentList.sf_alipay.sum_refund_amount);
                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
                MessageBox.Show(result_json["msg"].ToString(), "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                inResult = result_json["msg"].ToString();
                ret = false;
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================操作员订单查询===================", "End");
            return ret;
        }

        //18 检查更新
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inResult"></param>
        /// <param name="version"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool ApiGetNewVersion(string type, out string inResult,out string version,out string url)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/site/get_latest";

            bool ret = false;
            url = string.Empty;
            version = string.Empty;

            LogManager.WriteLogTran(LogType.Message, "===================检查更新===================", "Start");
            string httppara = string.Format("type={0}", type);

            LogManager.WriteLogTran(LogType.Message, "ApiGetNewVersion Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }


            if (result_json["code"].ToString() == "1")
            {


                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "msg = ", inResult);

                version = result_json["data"]["version"].ToString();
                LogManager.WriteLogTran(LogType.Message, "version = ", version);

                url = result_json["data"]["link"].ToString();
                LogManager.WriteLogTran(LogType.Message, "link = ", url);

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {

            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================检查更新===================", "End");
            return ret;

            
        }

        //02 退出
        public static bool ApiExit(string inToken, out string inResult)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/site/logout";

            bool ret = false;
    

            LogManager.WriteLogTran(LogType.Message, "===================退出===================", "Start");
            string httppara = string.Format("key={0}", inToken);

            LogManager.WriteLogTran(LogType.Message, "ApiExit Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }


            if (result_json["code"].ToString() == "1")
            {


                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "msg = ", inResult);

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {

            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================退出===================", "End");
            return ret;


        }

        //20 交接班
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inToken"></param>
        /// <param name="operator_id"></param>
        /// <param name="time_start"></param>
        /// <param name="time_end"></param>
        /// <param name="inResult"></param>
        /// <returns></returns>
        public static bool ApiHandover(string inToken, string operator_id, string time_start, string time_end, out string inResult)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/handover";
                                    
            bool ret = false;
            
            LogManager.WriteLogTran(LogType.Message, "===================交接班===================", "Start");
            string httppara = string.Format("key={0}&operator_id={1}&&time_start={2}&time_end={3}", inToken, operator_id, time_start, time_end);

            LogManager.WriteLogTran(LogType.Message, "ApiHandover Send = ", httppara.ToString());

            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "Result = ", inResult.ToString());
            JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
            if (result_json.Count == 0)
            {
                //log
                inResult = null;
                return false;
            }


            if (result_json["code"].ToString() == "1")
            {   
                inResult = result_json["msg"].ToString();
                LogManager.WriteLogTran(LogType.Message, "msg = ", inResult);

                string data = result_json["data"].ToString();
                LogManager.WriteLogTran(LogType.Message, "data = ", data.ToString());

                ret = true;
            }
            else if (result_json["code"].ToString() == "0")
            {
           
            }
            else
            {
                //log
                inResult = null;
                ret = false;
            }

            LogManager.WriteLogTran(LogType.Message, "===================交接班===================", "End");
            return ret;

        }

        //07验证密钥
        public static bool ApiVerifyKey(string key, string inToken, out string inResult)
        {
            const string httpurl = "http://cashier.cdsoftware.cn/index.php/api/user/verify_key";
            bool ret = false;

            //key = "bf0ac37accfb6ed19d4676a54d6aaaf0";

            //1 json
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("timestamp", GetTimeStamp());
            string jsonData = JsonConvert.SerializeObject(dic);
            LogManager.WriteLogTran(LogType.Message, "ApiVerifyKey jsonData= ", jsonData.ToString());

            //2 encrypt
            string encryptData = AESEncrypt(jsonData, key);

            string httppara = string.Format("key={0}&data={1}", inToken, encryptData);
            LogManager.WriteLogTran(LogType.Message, "ApiVerifyKey httppara= ", httppara.ToString());

            //3 send
            inResult = HttpPost(httpurl, httppara, null);
            LogManager.WriteLogTran(LogType.Message, "ApiVerifyKey Result= ", inResult.ToString());

            try
            {
                //4 get return json
                JObject result_json = JsonConvert.DeserializeObject(inResult) as JObject;
                inResult = result_json["msg"].ToString();

                if (result_json.Count == 0)
                {
                    return false;
                }

                //5 judge
                if (result_json["code"].ToString() == "1")
                {
                    inResult = result_json["msg"].ToString();
                    LogManager.WriteLogTran(LogType.Message, "inResult= ", inResult.ToString());

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
                inResult = null;
                ret = false;
            }

            return ret;
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

        public static string AESDecrypt(String Data, String Key)
        {
            MYLIB mylib = new MYLIB();

            // 256-AES key      
            byte[] keyArray = new byte[Key.Length / 2];
            mylib.GetHexFromString(Key, keyArray);

            //"112233" -> 0x11, 0x22, 0x33
            byte[] toEncryptArray = new byte[Data.Length / 2];
            mylib.GetHexFromString(Data, toEncryptArray);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.IV = Encoding.UTF8.GetBytes(AES_IV.Substring(0, 16));
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
                //MessageBox.Show(ex.ToString());
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
