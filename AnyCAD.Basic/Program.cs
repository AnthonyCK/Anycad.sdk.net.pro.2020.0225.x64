using System;
using System.Collections.Generic;
using System.Linq;
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
            string email = "3336982070@qq.com";
            string uuid = "91310120MA1HKMQE30-20200508";
            string sn = "d951f4b172825d9b02b46d5bef07323d";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AnyCAD.Platform.GlobalInstance.Application.SetLogFileName(new AnyCAD.Platform.Path("anycad.net.sdk.log"));
            AnyCAD.Platform.GlobalInstance.RegisterSDK(email, uuid, sn);
            Application.Run(new FormMain());
            //Application.Run(new TestForm());
        }
    }
}
