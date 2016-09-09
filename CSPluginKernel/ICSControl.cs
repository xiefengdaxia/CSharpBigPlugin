using System;
using System.Drawing;

namespace CSPluginKernel {

	public interface IApplicationObject {
		void Alert( string msg );// 产生一条信息
		void ShowInStatusBar( string msg ); // 将指定的信息显示在状态栏

		void SetDelegate( Delegates whichOne , EventHandler target );

		IDocumentObject QueryCurrentDocument(); // 获取当前使用的文档对象
		IDocumentObject[] QueryDocuments(); // 获取所有的文档对象
	}

	/// <summary>
	/// 编辑器对象必须实现这个接口
	/// </summary>
	public interface IDocumentObject {

		string SelectionText { get; set; }
		Color SelectionColor { get; set; }
		Font SelectionFont { get; set; }
		int SelectionStart { get; set; }
		int SelectionLength { get; set; }
		string SelectionRTF { get; set; }

		bool HasChanges { get; }

		void Select( int start , int length );
		void AppendText( string str );

		void SaveFile( string fileName );
		void SaveFile();

		void OpenFile( string fileName );

		void CloseFile();

	}

	public enum Delegates {
		Delegate_ActiveDocumentChanged ,
	}

}