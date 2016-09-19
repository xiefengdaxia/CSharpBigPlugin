using CSPluginKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BuildAndExecuteSQL
{
    [PluginInfo("批量生成和执行sql脚本", "1.0", "xiefeng", "1005928472@qq.com", true)]
    class BuildAndExecuteSQLPlugin : IPlugin
    {
        public BuildAndExecuteSQLPlugin() { }
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

