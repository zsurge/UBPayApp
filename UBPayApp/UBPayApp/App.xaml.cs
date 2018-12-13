using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace UBPayApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public EventWaitHandle ProgramStarted { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            bool createNew;
            ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, "UB", out createNew);

            if (!createNew)
            {
                MessageBox.Show("程序已在运行中","系统提示",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                App.Current.Shutdown();
                Environment.Exit(0);
            }
            base.OnStartup(e);
        }
    }
}
