using CSPluginKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETS_database_structure
{
    [PluginInfo("数据库字典生成器", "1.0", "xiefeng", "1005928472@qq.com", true)]
    public class ETS_database_structurePlugin:IPlugin
    {
        public ETS_database_structurePlugin() { }
        #region IPlugin 成员

        public ConnectionResult Connect(IApplicationObject app)
        {
            try
            {
                _App = app;

                _App.SetDelegate(Delegates.Delegate_ActiveDocumentChanged, new EventHandler(this.ActiveDocumentChanged));
                return ConnectionResult.Connection_Success;
            }
            catch
            {
                return ConnectionResult.Connection_Failed;
            }
        }

        private void ActiveDocumentChanged(object sender, EventArgs e)
        {
            _CurDoc = _App.QueryCurrentDocument();
        }

        public void Run()
        {
            _App.ShowInStatusBar("This is my first plugin!");
            Form1 frm = new Form1(_CurDoc);
            frm.ShowDialog();
        }

        public void OnLoad()
        {
            _CurDoc = _App.QueryCurrentDocument();
        }

        public void OnDestory()
        {

        }

        #endregion


        private IApplicationObject _App;
        private IDocumentObject _CurDoc;
    }
}
