using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InsertSqlLookValue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string sqlFields;
        public string fieldsValues;
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            var sql = richTextBox1.Text;
            string[] arrs = sql.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            sqlFields = arrs[1];
            fieldsValues = arrs[3];
            string[] ArrayFields = sqlFields.Replace(" ", "").Split(',');
            string[] ArrayValuess = fieldsValues.Replace(" ", "").Split(',');
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < ArrayFields.Length; i++)
            {
                dict.Add(ArrayFields[i], ArrayValuess[i]);
                RichShow(ArrayFields[i], ArrayValuess[i],richTextBox2);
            }
            
        }
         //需要插入的字符和颜色
        void RichShow(string field,string value,RichTextBox rtb)
        {
            rtb.SelectionColor = Color.Purple;
            rtb.AppendText(field + ":");
            if (value.Contains("'"))
            {
                rtb.SelectionColor = Color.Green;
                rtb.AppendText(value+"\n");
            }
            else if (value.Contains("null") || value.Contains("NULL"))
            {
                rtb.SelectionColor = Color.Orange;
                rtb.AppendText(value+"\n");
            }
            else
            {
                rtb.SelectionColor = Color.Blue;
                rtb.AppendText(value + "\n");
            }
        }
        public string replaceNewLine(string text)
        {
            return text.Replace("\n", " ");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            int index = this.richTextBox1.SelectionStart;    //记录修改的位置
            this.richTextBox1.SelectAll();
            this.richTextBox1.SelectionColor = Color.Black;
            //SQL关键字
            string[] keystr ={ "select ", "from ", "where ", " and ", " or ",
                               " order ", " by ", " desc ", " when ", " case ",
                               " then ", " end ", " on ", " in ", " is ", " else ",
                               " left ", " join ", " not ", " null " };
            for (int i = 0; i < keystr.Length; i++)
                this.getbunch(keystr[i], this.richTextBox1.Text);

            this.richTextBox1.Select(index, 0);     //返回修改的位置
            this.richTextBox1.SelectionColor = Color.Black;
        }
        public int getbunch(string p, string s)  //给关键字上色
        {
            int cnt = 0;
            int M = p.Length;
            int N = s.Length;
            char[] ss = s.ToCharArray(), pp = p.ToCharArray();
            if (M > N) return 0;
            for (int i = 0; i < N - M + 1; i++)
            {
                int j;
                for (j = 0; j < M; j++)
                {
                    if (ss[i + j] != pp[j]) break;
                }
                if (j == p.Length)
                {
                    this.richTextBox1.Select(i, p.Length);
                    this.richTextBox1.SelectionColor = Color.Blue;
                    cnt++;
                }
            }
            return cnt;
        }
    }
}
