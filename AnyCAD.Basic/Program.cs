using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Windows.Forms;

namespace AnyCAD.Basic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string email = ConfigurationManager.AppSettings["Email"];
            string uuid = ConfigurationManager.AppSettings["uuid"];
            string sn = ConfigurationManager.AppSettings["sn"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AnyCAD.Platform.GlobalInstance.Application.SetLogFileName(new AnyCAD.Platform.Path("anycad.net.sdk.log"));
            AnyCAD.Platform.GlobalInstance.RegisterSDK(email, uuid, sn);
            Application.Run(new FormMain());
            //Application.Run(new TestForm());
        }
    }
}
