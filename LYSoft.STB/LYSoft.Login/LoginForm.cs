using DevExpress.XtraEditors;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using LYSoft.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace LYSoft.Login
{
    public partial class LoginForm : XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
            this.InitializationAttribute();
            IninCode();
        }

        public void IninCode()
        {
            string vc = "";
            Random rNum = new Random();//随机生成类
            int num1 = rNum.Next(0, 9);//返回指定范围内的随机数
            int num2 = rNum.Next(0, 9);
            int num3 = rNum.Next(0, 9);
            int num4 = rNum.Next(0, 9);

            int[] nums = new int[4] { num1, num2, num3, num4 };
            for (int i = 0; i < nums.Length; i++)//循环添加四个随机生成数
            {
                vc += nums[i].ToString();
            }
            buttonEdit2.Text = vc;
        }



        /// <summary>
        /// 初始化属性
        /// </summary>
        public void InitializationAttribute()
        {
            this.BackgroundImage = Properties.Resources.banner1212;
            ProBar.LookAndFeel.SkinName = "Metropolis";
        }

        

       


        /// <summary>
        /// 登录时候委托把信息更新到UI上
        /// </summary>
        private delegate void ShowLoginInfoEventHandler(ProgressBarControl Conts,Label Tips, string Info);

        /// <summary>
        /// 委托更新UI界面的值
        /// </summary>
        /// <param name="Message">当前消息</param>
        private void ShowLoinLogInfo(ProgressBarControl Conts, Label Tips, string Message)
        {
            if (this.InvokeRequired)
            {
                ShowLoginInfoEventHandler _d = new ShowLoginInfoEventHandler(this.ShowLoinLogInfo);
                this.Invoke(_d, new object[] { ProBar, Tips, Message });
            }
            else
            {
                try
                {
                    Tips.Text = Message;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(10);
                    Conts.PerformStep();
                    //msg1.AddLine(Message);
                }
                catch
                {
                    return;
                }
            }
        }
        

        /// <summary>
        /// 登录按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserName = g1_user2.Text;
            string pawss = g1_pwd.Text;
            string code = buttonEdit1.Text;
            if(code == "")
            {
                DevExpress.xtraMessage.ShowTip("请输入验证码.");
                Tips.Text = "请输入验证码.";
                return;
            }
            if(buttonEdit1.Text != buttonEdit2.Text)
            {
                DevExpress.xtraMessage.ShowTip("验证码错误，请重试.");
                Tips.Text = "验证码错误，请重试.";
                return;
            }
            Tips.Visible =true;
            string sql = $"SELECT * FROM T_A_DATA_USER where DLZH='{UserName}' and MM ='{pawss}'";
            DataTable tab = SQLiteHelper.QueryDataTable(sql);
            T_A_DATA_USER user = Converts.Convert<T_A_DATA_USER>(tab);
            if (user == null || user.ID == 0)
            {
                DevExpress.xtraMessage.ShowTip("密码错误,请重试.");
                Tips.Text = "密码错误,请重试.";
                return;
            }
            else
            {
                MyApps.User = user;
                this.Hide();
                RibbonFormMain main = new RibbonFormMain(user.ID);
                main.ShowDialog();
                return;
            }
        }


        /// <summary>
        /// 点击退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //处理Windows消息后关闭所有窗口
            Process.GetCurrentProcess().Kill();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Application.DoEvents();
        }

       

        private void g1_user_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void g1_pwd_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           string paw = g1_pwd.Text;

        }


        private void g1_pwd_TextChanged(object sender, EventArgs e)
        {
        }

        private void g1_pwd_KeyPress(object sender, KeyPressEventArgs e)
        {
          
        }


    
        


        #region 移动窗口

        Point pt1;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (e.Y > 120)
            {
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                pt1 = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (e.Y > 120)
                {
                    pt1 = e.Location;
                    return;
                }
                int _w1 = e.X - pt1.X;
                int _h1 = e.Y - pt1.Y;
                this.Location = new Point(this.Location.X + _w1, this.Location.Y + _h1);
            }
        }




        #endregion



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register from = new Register();
            from.ShowDialog();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            /*string path = Path.Combine(Application.StartupPath, "FileHelper.exe");
            Process myProcess = new Process();
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(path);
            myProcessStartInfo.CreateNoWindow = true;
            myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo = myProcessStartInfo;
            myProcess.Start();
            */
        }
    }
}
