using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using LYSoft.ArcCore;
using LYSoft.ArcCore.Entity;
using LYSoft.Center;
using LYSoft.DataBase.SQLite;
using LYSoft.Domain;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace LYSoft.Component
{
    public partial class AddFace : XtraForm
    {
        private bool Isbrack = true;
        public T_A_DATA_RYXX model;

        public delegate void ShowBingTableHandler(FaceEntity temp);

        public object lockpic = new object();
        public AddFace(T_A_DATA_RYXX RYXX=null)
        {
            InitializeComponent();
            dateEdit1.Text = DateTime.Now.ToString("yyyy-MM-dd");
            model = RYXX;
            if (model != null)
            {
                textEdit1.Text = model.XM;
                textEdit2.Text = model.XH;
                textEdit3.Text = model.XB;
                textEdit4.Text = model.SZBJ.ToString();
                textEdit5.Text = model.LXDH;
                dateEdit1.Text = model.CSRQ;
                if (model.TZM !="" && model.TZM != null)
                {
                    textEdit6.Text = "已采集";
                }
                else
                {
                    textEdit6.Text = "未采集";
                }
                string path = Application.StartupPath + "\\" + model.IMG;
                if (File.Exists(path))
                {
                    pictureBox1.Image = Image.FromFile(path);
                }
            }
            else
            {
                this.Text = "添加人脸";
            }
        }

        private void AddFace_Load(object sender, EventArgs e)
        {
            //Thread th = new Thread(GetImgAgeAndSex);
            //th.IsBackground = true;
            //th.Start();
        }


        public void ShowInfo(FaceEntity temp)
        {
            if (this.InvokeRequired)
            {
                ShowBingTableHandler BingTable = new ShowBingTableHandler(this.ShowInfo);
                this.Invoke(BingTable, new object[] {temp });
            }
            else
            {
                textEdit3.Text = temp?.Sex == 0 ? "男" : "女";
                textEdit4.Text = temp?.Ages.ToString();
            }

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            lock (lockpic)
            {
                try
                {
                    Bitmap img = this.cameraControl1.TakeSnapshot();
                    string catalog = MyApps.Imgpath + "\\" + DateTime.Now.ToString("yyyy-MM-dd");
                    if (!IOHelper.DireExist(catalog))
                    {
                        IOHelper.CreateDirectory(catalog);
                    }
                    string path = Path.Combine(catalog, DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".png");
                    byte[] feature = ArcHelper.FaceFeature(img);  //获取特征码
                    string base64 = feature.BytesToBase64();
                    if(base64 == "")
                    {
                        xiaoid.forms.xtraMessage.ShowError("人脸特征获取失败.");
                    }
                    img.Save(path);  //保存图片
                    if(model == null)  //新增数据
                    {
                        model = new T_A_DATA_RYXX();
                        model.XM = textEdit1.Text;
                        model.XH = textEdit2.Text;
                        model.XB = textEdit3.Text;
                        model.SZBJ = textEdit4.Text;
                        model.LXDH = textEdit5.Text;
                        model.CSRQ = dateEdit1.Text;
                        model.TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        model.TZM = base64;
                        model.IMG = Regex.Split(path, "Debug", RegexOptions.IgnoreCase).LastOrDefault();
                        string sql = model.AddToSQL() + "   ;select last_insert_rowid()";
                        string ryid = SQLiteHelper.ExecuteToKey(sql).ToString();
                        if (ryid == "")
                        {
                            xiaoid.forms.xtraMessage.ShowError("人脸注册失败.");
                        }
                        else
                        {
                            model.ID = ryid.ObjectToInt();
                            CacheHelper.Add(model);
                            xiaoid.forms.xtraMessage.ShowTip("人脸保存成功.");
                            cameraControl1.Dispose();  //释放摄像头资源
                            this.Close();
                        }
                    }
                    else
                    {
                        model.XM = textEdit1.Text;
                        model.XH = textEdit2.Text;
                        model.XB = textEdit3.Text;
                        model.SZBJ = textEdit4.Text;
                        model.LXDH = textEdit5.Text;
                        model.CSRQ = dateEdit1.Text;
                        model.TZM = base64;
                        model.IMG = Regex.Split(path, "Debug", RegexOptions.IgnoreCase).LastOrDefault();
                        int count = model.UpdateNotNull();
                        if (count <=0)
                        {
                            xiaoid.forms.xtraMessage.ShowError("人脸更新失败.");
                        }
                        else
                        {
                            CacheHelper.Add(model);
                            xiaoid.forms.xtraMessage.ShowTip("人脸更新成功.");
                            cameraControl1.Dispose();  //释放摄像头资源
                            Isbrack = false;
                            this.Close();
                        }
                    }

                   
                }
                catch (Exception ex)
                {
                    xiaoid.forms.xtraMessage.ShowError(ex.ToString());
                    return;
                }
            }
        }


        /// <summary>
        /// 获取图片中性别和年龄
        /// </summary>
        public void GetImgAgeAndSex()
        {
            while (Isbrack)
            {
                try
                {
                    Bitmap img = this.cameraControl1.TakeSnapshot(); //获取当前帧
                    FaceEntity model = ArcHelper.PartFaceAgeandSex(img);
                    ShowInfo(model);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteError(ex.ToString());
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }

        }

        private void AddFace_FormClosed(object sender, FormClosedEventArgs e)
        {
            Isbrack = false;
            cameraControl1.Dispose();
        }
    }
}
