using System;
using System.Drawing;

namespace CSPluginKernel {

	public interface IApplicationObject {
		void Alert( string msg );// ����һ����Ϣ
		void ShowInStatusBar( string msg ); // ��ָ������Ϣ��ʾ��״̬��

		void SetDelegate( Delegates whichOne , EventHandler target );

		IDocumentObject QueryCurrentDocument(); // ��ȡ��ǰʹ�õ��ĵ�����
		IDocumentObject[] QueryDocuments(); // ��ȡ���е��ĵ�����
	}

	/// <summary>
	/// �༭���������ʵ������ӿ�
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