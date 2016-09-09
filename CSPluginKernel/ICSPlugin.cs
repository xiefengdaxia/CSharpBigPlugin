using System;

namespace CSPluginKernel {

	/// <summary>
	/// ������Ĳ������ʵ������ӿ�
	/// </summary>
	public interface IPlugin {
		ConnectionResult Connect( IApplicationObject app );
		void OnDestory();
		void OnLoad();
		void Run();
	}

	/// <summary>
	/// ��ʾ��������������ӵĽ��
	/// </summary>
	public enum ConnectionResult {
		Connection_Success ,
		Connection_Failed
	}

}