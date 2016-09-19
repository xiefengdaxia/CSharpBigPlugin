using CSPluginKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressProfiler
{
    [PluginInfo("事件探查器v2.1", "1.0", "xiefeng", "1005928472@qq.com", true)]
    class ExpressProfilerPlugin : IPlugin
    {
        public ExpressProfilerPlugin() { }

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
            MainForm frm = new MainForm(_CurDoc);
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
