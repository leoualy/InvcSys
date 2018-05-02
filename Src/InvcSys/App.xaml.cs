using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InvcSys
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string []args = e.Args;
            if (args.Count() <= 0)
            {
                // 在ERP不传参数的情况下启动，按配置给定的参数启动
                BlueViewModel.RESV_Name_ID = ConfigurationManager.AppSettings["Test_RESV_Name_ID"];
                return;
            }
            BlueViewModel.RESV_Name_ID = args[0];
        }
    }
}
