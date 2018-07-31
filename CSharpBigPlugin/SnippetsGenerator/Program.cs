using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Reflection;
namespace SnippetsGenerator
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
		private delegate void SetPropertyThreadSafeDelegate<TResult>(Control @this, Expression<Func<TResult>> property, TResult value);

		/// <summary>
		/// 后台线程更新UI功能
		/// 调用方法如 label1.SetPropertyThreadSafe(() => label1.Text, json1);
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="this">当前控件</param>
		/// <param name="property">属性名</param>
		/// <param name="value">属性值</param>
		public static void SetPropertyThreadSafe<TResult>(this Control @this, Expression<Func<TResult>> property, TResult value)
		{
			var propertyInfo = (property.Body as MemberExpression).Member
				as PropertyInfo;

			if (propertyInfo == null ||
				!@this.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
				@this.GetType().GetProperty(
					propertyInfo.Name,
					propertyInfo.PropertyType) == null)
			{
				throw new ArgumentException("lambda表达式 'property' 必须是一个有效的属性");
			}

			if (@this.InvokeRequired)
			{
				@this.Invoke(new SetPropertyThreadSafeDelegate<TResult>
				(SetPropertyThreadSafe),
				new object[] { @this, property, value });
			}
			else
			{
				@this.GetType().InvokeMember(
					propertyInfo.Name,
					BindingFlags.SetProperty,
					null,
					@this,
					new object[] { value });
			}
		}
	}
}
