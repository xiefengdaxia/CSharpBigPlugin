using System;

namespace CSPluginKernel {

	/// <summary>
	/// 本程序的插件必须实现这个接口
	/// </summary>
	public interface IPlugin {
		ConnectionResult Connect( IApplicationObject app );
		void OnDestory();
		void OnLoad();
		void Run();
	}

	/// <summary>
	/// 表示插件与主程序连接的结果
	/// </summary>
	public enum ConnectionResult {
		Connection_Success ,
		Connection_Failed
	}

}