using LYSoft.Center;
using System;
using System.Threading;
using System.Windows.Forms;

namespace LYSoft.Main
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                SetSkin();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ThreadException += Application_ThreadException;
                //不执行，
                Application.Run(new RibbonFormMain(0));
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex.ToString());
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            LogHelper.WriteError(e.ToString());
        }

     
        
      

        public static void SetSkin()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Metropolis";
        }

       
        

    }
}
