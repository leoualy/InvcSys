using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Configuration;

namespace InvcSys
{
    internal class ApiServer
    {
        static HttpSelfHostServer mServer;
        internal static void Start()
        {
            string url = ConfigurationManager.AppSettings["ApiAddress"];
            var config = new HttpSelfHostConfiguration(url);

            config.Routes.MapHttpRoute("Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            mServer = new HttpSelfHostServer(config);
            try
            {
                mServer.OpenAsync().Wait();
            }
            catch (Exception e)
            {
                throw new Exception("启动api服务时异常:" + e.Message);
            }
            
        }

        internal static void Close()
        {
            try
            {
                mServer.CloseAsync();
            }
            catch (Exception e)
            {
                throw new Exception("停止api服务时异常:" + e.Message);
            }
            
        }
    }
}
