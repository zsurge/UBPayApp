using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBPayApp
{
    class BaiduOcr
    {
        // 设置APPID/AK/SK
        string APP_ID = "14200096";
        string API_KEY = "jQnsoiW8nt3dkAixiQVGGyRS";
        string SECRET_KEY = "xozGfR4ik5avpLFoR8BGd8t6ZLnMrKSE";

        public string getTextFromImage(string url)
        {
            try
            {
                var image = File.ReadAllBytes(url);

                var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
                client.Timeout = 5000;//修改超时时间

                // 如果有可选参数
                var options = new Dictionary<string, object>{
                {"language_type", "CHN_ENG"},
                {"detect_direction", "true"},
                {"detect_language", "true"},
                {"probability", "true"}
            };

                // 带参数调用通用文字识别, 图片参数为本地图片
                var result = client.GeneralBasic(image, options);

                return result.ToString();
            }
            catch(Exception err)
            {
                return null;
            }
        }
    }
}
