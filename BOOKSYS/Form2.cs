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

namespace BOOKSYS
{
	public partial class Form2 : Form
	{
		string strcon = @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MBOOK;Data Source=localhost\SQLEXPRESS";
		public Form2()
		{
			InitializeComponent();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStrSelect = "select [BookID],[ISBN],[BookName],[Publisher],[Price],[LTime] from [RBL] where [ReaderID]='" + textBox1.Text.Trim() + "'";
			try
			{
				SqlDataAdapter adapter = new SqlDataAdapter(sqlStrSelect, conn);
				DataSet dstable = new DataSet();
				adapter.Fill(dstable, "testTable");
				dataGridView1.DataSource = dstable.Tables["testTable"];
				dataGridView1.Show();
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

		private void button2_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
			{
				MessageBox.Show("借书证号，ISBN，图书ID输入完整！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			SqlCommand cmd = new SqlCommand("Book_Borrow", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			SqlParameter inReaderID = new SqlParameter("@in_ReaderID", SqlDbType.Char, 8);
			inReaderID.Direction = ParameterDirection.Input;
			inReaderID.Value = textBox1.Text.Trim();
			cmd.Parameters.Add(inReaderID);
			SqlParameter inISBN = new SqlParameter("@in_ISBN", SqlDbType.Char, 18);
			inISBN.Direction = ParameterDirection.Input;
			inISBN.Value = textBox2.Text.Trim();
			cmd.Parameters.Add(inISBN);
			SqlParameter inBookID = new SqlParameter("@in_BookID", SqlDbType.Char, 8);
			inBookID.Direction = ParameterDirection.Input;
			inBookID.Value = textBox3.Text.Trim();
			cmd.Parameters.Add(inBookID);
			SqlParameter outReturn = new SqlParameter("@out_str", SqlDbType.Char, 30);
			outReturn.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(outReturn);
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show(outReturn.Value.ToString());
			}
			catch(Exception ex)
			{
				MessageBox.Show("借书出错！"+ ex.Message);
			}
			finally
			{
				conn.Close();
				button1_Click(null, null);
			}
		}
	}
}
