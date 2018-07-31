using LitJson2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnippetsGenerator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		
		int count = 0, currentNo = 0;
		private void button1_Click(object sender, EventArgs e)
		{
			Thread worker = new Thread(delegate ()
			{
				run();
			});
			worker.IsBackground = true;
			worker.Start();
		}

		private void run()
		{
			var path = "D:\\Desktop\\赛思\\Xrm代码片段\\XrmJS.json";
			string jsonText = "";
			string templateString = "";
			using (StreamReader sr = new StreamReader(path))
			{
				jsonText = sr.ReadToEnd();

			}
			JsonData jd = JsonMapper.ToObject(jsonText);
			count = jd.Count;
			
			for (int i = 0; i < jd.Count; i++)
			{
				currentNo = i + 1;
				//MessageBox.Show(jd[i][0]["prefix"].ToString());
				var lableText = string.Format("当前{0},总共{1}", currentNo, count);
				label1.SetPropertyThreadSafe(() => label1.Text, lableText);

				var snippet = string.Format(textBox1.Text, jd[i][0]["prefix"].ToString(), replaceString(getBody(jd[i][0]["body"])), replaceString(jd[i][0]["description"].ToString()));
				richTextBox1.SetPropertyThreadSafe(() => richTextBox1.Text, snippet);
				CSHelper.saveFile(snippet, jd[i][0]["prefix"].ToString(), "snippet");
			}


		}

		private string getBody(JsonData Jdbody)
		{
			string ret = "";

			for (int i = 0; i <Jdbody.Count; i++)
			{
				if (Jdbody[i].ToString() == string.Empty)
					continue;
				ret += Jdbody[i]+ (Jdbody[i].ToString().EndsWith(";")?"":";")+
					(i==(Jdbody.Count-1)?"": "\n") ;
			}
			return ret;
		}
		private string replaceString(string content)
		{
			return content.Replace("${", "").Replace("}\"","\"").Replace("})",")");

			//return content
			//	.Replace("${fieldname}", "字段名")
			//	.Replace("${qvcontrolname}", "快速视图控件名")
			//	.Replace("${controlname}", "控件名")
			//	.Replace("${functionName}", "功能名")
			//	;
		}
	}
}
