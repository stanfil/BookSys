using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace BOOKSYS
{
	public partial class Form3 : Form
	{
		string strcon = BOOKSYS.Properties.Settings.Default.MBOOKConnectionString;
		public Form3()
		{
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e)
		{
			// TODO: 这行代码将数据加载到表“mBOOKDataSet1.RBL”中。您可以根据需要移动或删除它。
			this.rBLTableAdapter.Fill(this.mBOOKDataSet1.RBL);

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text == "")
			{
				MessageBox.Show("请输入图书ID");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "Delete From [TLend] where [BookID]=@BookID";
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			cmd.Parameters.Add("@BookID", SqlDbType.Char, 10).Value = textBox1.Text.Trim();
			try
			{
				conn.Open();
				int a = cmd.ExecuteNonQuery();
				this.rBLTableAdapter.Fill(this.mBOOKDataSet1.RBL);
				if (a == 3)
				{
					MessageBox.Show("删除成功！");
				}
				else
				{
					MessageBox.Show("数据库中没有该书！");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				conn.Close();
			}
		}
	}
}
