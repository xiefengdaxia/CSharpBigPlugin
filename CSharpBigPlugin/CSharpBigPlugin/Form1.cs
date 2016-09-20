using CSPluginKernel;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CSharpBigPlugin
{
    public partial class Form1 : MetroAppForm ,CSPluginKernel.IApplicationObject
    {
        private ArrayList plugins = new ArrayList();
        //private System.Windows.Forms.MenuItem menuItem6;
        private ArrayList piProperties = new ArrayList();
        public Form1()
        {
            InitializeComponent();
            //this.Load += new EventHandler(Form1_Load);
            //menuItem2_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("1");
                this.LoadAllPlugins();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        /// <summary>
        /// CSPluginKernel.IPlugin  可以用XML来配置，以实现动态
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private bool IsValidPlugin(Type t)
        {
            bool ret = false;
            Type[] interfaces = t.GetInterfaces();
            foreach (Type theInterface in interfaces)
            {
                if (theInterface.FullName == "CSPluginKernel.IPlugin")
                {
                    ret = true;
                    break;
                }
            }
            return ret;
        }
        private void LoadAllPlugins()
        {
            string[] files = Directory.GetFiles(Application.StartupPath + "\\plugins\\");
            int i = 0;
            PluginInfoAttribute typeAttribute = new PluginInfoAttribute();
            foreach (string file in files)
            {
                string ext = file.Substring(file.LastIndexOf("."));
                if (ext != ".dll") continue;
                try
                {
                    Assembly tmp = Assembly.LoadFile(file);
                    Type[] types = tmp.GetTypes();
                    bool ok = false;
                    foreach (Type t in types)
                        if (IsValidPlugin(t))
                        {
                            plugins.Add(tmp.CreateInstance(t.FullName));
                            object[] attbs = t.GetCustomAttributes(typeAttribute.GetType(), false);
                            PluginInfoAttribute attribute = null;
                            foreach (object attb in attbs)
                            {
                                if (attb is PluginInfoAttribute)
                                {
                                    attribute = (PluginInfoAttribute)attb;
                                    attribute.Index = i;
                                    i++;
                                    ok = true;
                                    break;
                                }
                            }

                            if (attribute != null) this.piProperties.Add(attribute);
                            else throw new Exception("未定义插件属性");

                            if (ok) break;
                        }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            //随机产生颜色
            eMetroTileColor[] colors = Enum.GetValues(typeof(eMetroTileColor)) as eMetroTileColor[];
            Random random = new Random();
            foreach (PluginInfoAttribute pia in piProperties)
            {
                //MenuItem tmp = menuItem6.MenuItems.Add(pia.Name + " " + pia.Version + " [ " + pia.Author + " ]");
                MetroTileItem MTI = new MetroTileItem();
                //随机赋值一个颜色
                MTI.TileColor = colors[random.Next(0, colors.Length)];// eMetroTileColor.DarkGreen;
                MTI.Name = pia.Name;
                MTI.TitleText = pia.Name;// +pia.Author + pia.Version;
                //MTI.Text = pia.Author;//
                MTI.Symbol = "☺";
                MTI.TitleTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                MTI.Click += new EventHandler(RunPlugin);
                pia.Tag = MTI;
                this.itemContainer1.SubItems.Add(MTI);
            }

            foreach (IPlugin pi in plugins)
            {
                if (pi.Connect((IApplicationObject)this) == ConnectionResult.Connection_Success)
                {
                    pi.OnLoad();
                }
                else
                {
                    MessageBox.Show("Can not connect plugin!");
                }
            }
        }

        private void RunPlugin(object sender, EventArgs e)
        {
            foreach (PluginInfoAttribute pia in piProperties)
                if (pia.Tag.Equals(sender))
                    ((IPlugin)plugins[pia.Index]).Run();
        }


        #region IApplicationObject 成员

        public void SetDelegate(Delegates whichOne, EventHandler target)
        {
            //switch (whichOne)
            //{
            //    case Delegates.Delegate_ActiveDocumentChanged:
            //        this.tabDocs.SelectedIndexChanged += target;
            //        break;
            //}
        }

        public IDocumentObject[] QueryDocuments()
        {
            ArrayList list = new ArrayList();
            //for (int i = 0; i < this.tabDocs.TabPages.Count; i++)
            //    list.Add(tabDocs.TabPages[i].Tag);
            return (IDocumentObject[])list.ToArray();
        }

        public IDocumentObject QueryCurrentDocument()
        {
            //if (tabDocs.SelectedIndex != -1)
            //    return (IDocumentObject)this.tabDocs.SelectedTab.Tag;
            //else
                return null;
        }

        public void ShowInStatusBar(string msg)
        {
           // _Status.Panels[0].Text = msg;
        }

        public void Alert(string msg)
        {
            MessageBox.Show(msg);
        }
        #endregion

        private void metroShell1_SettingsButtonClick(object sender, EventArgs e)
        {
            try
            {
                string path = Application.StartupPath + "\\lxerp.ini";
                System.Diagnostics.Process.Start(path); //打开此文件。
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\r" + ex.StackTrace,"错误提示");
            }
        }

        private void metroAppButton1_Click(object sender, EventArgs e)
        {
            eMetroTileColor[] colors = Enum.GetValues(typeof(eMetroTileColor)) as eMetroTileColor[];
            Random random = new Random();
            foreach (DevComponents.DotNetBar.Metro.MetroTileItem item in this.itemContainer1.SubItems)
            {
                item.TileColor = colors[random.Next(0, colors.Length)];
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Windows7Blue;
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.VisualStudio2012Light;
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.VisualStudio2012Dark;
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.VisualStudio2010Blue;
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.OfficeMobile2014;
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = eStyle.Office2016;
        }

    }
}
