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
	public partial class Form5 : Form
	{
		string strcon = BOOKSYS.Properties.Settings.Default.MBOOKConnectionString;
		public Form5()
		{
			InitializeComponent();
		}

		private void Form5_Load(object sender, EventArgs e)
		{
			// TODO: 这行代码将数据加载到表“mBOOKDataSet2.TBook”中。您可以根据需要移动或删除它。
			this.tBookTableAdapter.Fill(this.mBOOKDataSet2.TBook);

		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
			{
				MessageBox.Show("请输入完整！");
				return;
			}
			string sqlStr = "insert [TBook]([ISBN],[BookName],[Author],[Publisher],[Price],[CNum],[SNum],[Summary]) values(@ISBN,@BookName,@Author,@Publisher,@Price,@CNum,@SNum,@Summary)";
			SqlConnection conn = new SqlConnection(strcon);
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			cmd.Parameters.Add("@ISBN", SqlDbType.Char, 18).Value = textBox6.Text.Trim();
			cmd.Parameters.Add("@BookName", SqlDbType.Char, 40).Value = textBox2.Text.Trim();
			cmd.Parameters.Add("@Author", SqlDbType.Char, 16).Value = textBox3.Text.Trim();
			cmd.Parameters.Add("@Publisher", SqlDbType.Char, 30).Value = textBox4.Text.Trim();
			cmd.Parameters.Add("@Price", SqlDbType.Float).Value = float.Parse(textBox5.Text.Trim());
			cmd.Parameters.Add("@CNum", SqlDbType.Int).Value = Convert.ToInt32(numericUpDown1.Value);
			cmd.Parameters.Add("@SNum", SqlDbType.Int).Value = Convert.ToInt32(numericUpDown2.Value);
			cmd.Parameters.Add("@Summary", SqlDbType.VarChar, 200).Value = textBox7.Text.Trim();
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("添加成功！");
				this.tBookTableAdapter.Fill(this.mBOOKDataSet2.TBook);
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
			if (textBox6.Text.Trim() == "")
			{
				MessageBox.Show("请输入图书ISBN！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "delete from [TBook] where [ISBN]=@ISBN";
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			cmd.Parameters.Add("@ISBN", SqlDbType.Char, 18).Value = textBox6.Text.Trim();
			try
			{
				conn.Open();
				int a = cmd.ExecuteNonQuery();
				this.tBookTableAdapter.Fill(this.mBOOKDataSet2.TBook);
				if (a == 1)
				{
					MessageBox.Show("删除成功！");
				}
				else
				{
					MessageBox.Show("数据库中没有此书！");
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

		private void button3_Click(object sender, EventArgs e)
		{
			if (textBox6.Text.Trim() == "")
			{
				MessageBox.Show("请输入图书ISBN！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "update [TBook] set ";
			if (textBox2.Text.Trim() != "")
			{
				sqlStr += "[BookName]='" + textBox2.Text.Trim() + "',";
			}
			if (textBox3.Text.Trim() != "")
			{
				sqlStr += "[Author]='" + textBox3.Text.Trim() + "',";
			}
			if (textBox4.Text.Trim() != "")
			{
				sqlStr += "[Publisher]='" + textBox4.Text.Trim() + "',";
			}
			if(textBox5.Text.Trim() != "")
			{
				sqlStr += " [Price]='" + textBox5.Text.Trim() + "',";
			}
			sqlStr += "[CNum]='" + Convert.ToInt32(numericUpDown1.Value) + "',[SNum]='"+ Convert.ToInt32(numericUpDown2.Value)+"',";
			if (textBox7.Text.Trim() != "")
			{
				sqlStr += "[Summary]='" + textBox7.Text.Trim() + "' where ISBN='" + textBox6.Text.Trim() + "'";
			}
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			try
			{
				conn.Open();
				int yxh = cmd.ExecuteNonQuery();
				if(yxh ==1)
				{
					MessageBox.Show("修改成功！");
				}
				else
				{
					MessageBox.Show("数据库中没有本书！");
				}
				this.tBookTableAdapter.Fill(this.mBOOKDataSet2.TBook);
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

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Trim() == "")
			{
				MessageBox.Show("请输入图书ISBN！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "select [ISBN],[BookName],[Author],[Publisher],[Price],[CNum],[SNum],[Summary] from [TBook] where [ISBN]='" + textBox1.Text.Trim() + "'";
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			try
			{
				conn.Open();
				SqlDataReader sdr = cmd.ExecuteReader();
				if (sdr.HasRows)
				{
					sdr.Read();
					textBox2.Text = sdr["BookName"].ToString();
					textBox3.Text = sdr["Author"].ToString();
					textBox4.Text = sdr["Publisher"].ToString();
					textBox5.Text = sdr["Price"].ToString();
					textBox6.Text = sdr["ISBN"].ToString();
					numericUpDown1.Value = Convert.ToInt32(sdr["CNum"]);
					numericUpDown2.Value = Convert.ToInt32(sdr["SNum"]);
					textBox7.Text = sdr["Summary"].ToString();
				}
				else
				{
					MessageBox.Show("没有这本书！");
				}
				if (!sdr.IsClosed)
				{
					sdr.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if (conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
				button1_Click(null, null);
			}
		}
	}
}
