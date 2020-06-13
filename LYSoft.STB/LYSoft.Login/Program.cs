using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LYSoft.Login
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitPath();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            Program.SetSkin();  //初始化程序皮肤
            Application.Run(new LoginForm()); //打开登录界面
        }
        
        public static void SetSkin()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SkinName = "Metropolis"; //DevExpress Style
        }

        private static void InitPath()
        {
            MyApps.LogErrorpath = Application.StartupPath + "\\Error";
            MyApps.Logpath = Application.StartupPath + "\\Log";
            MyApps.Imgpath = Application.StartupPath + "\\Images";
            MyApps.Temppath = Application.StartupPath + "\\Temp";
            MyApps.DataSource = Application.StartupPath + "\\DataSource";
            MyApps.DataName = "k01-20200530161625.db";
            //异常日志路径
            string Path = MyApps.LogErrorpath;
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }

            //程序运行路径
            Path = MyApps.Logpath;
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }

            //图片
            Path = MyApps.Imgpath;
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }

            //临时文件
            Path = MyApps.Temppath;
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }

            //创建数据库路径
            Path = MyApps.DataSource;
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            
        }


       

    }
}
