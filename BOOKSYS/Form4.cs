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
	public partial class Form4 : Form
	{
		string strcon = BOOKSYS.Properties.Settings.Default.MBOOKConnectionString;
		string FileNamePath = "";

		public Form4()
		{
			InitializeComponent();
		}

		private void Form4_Load(object sender, EventArgs e)
		{
			// TODO: 这行代码将数据加载到表“mBOOKDataSet.TReader”中。您可以根据需要移动或删除它。
			this.tReaderTableAdapter.Fill(this.mBOOKDataSet.TReader);

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
			{
				MessageBox.Show("请输入完整！");
				return;
			}
			string sqlStr = null;
			SqlConnection conn = new SqlConnection(strcon);
			if (FileNamePath != "")
			{
				sqlStr = "insert [TReader]([ReaderID],[Name],[Sex],[Spec],[Born],[Num],[Photo],[Detail]) values(@ReaderID,@Name,@Sex,@Spec,@Born,0,@Photo,@Detail)";
			}
			else
			{
				sqlStr = "insert [TReader]([ReaderID],[Name],[Sex],[Spec],[Born],[Num],[Detail]) values(@ReaderID,@Name,@Sex,@Spec,@Born,0,@Detail)";
			}
			SqlCommand cmd = new SqlCommand(sqlStr , conn);
			cmd.Parameters.Add("@ReaderID",SqlDbType.Char, 8).Value=textBox1.Text.Trim();
			cmd.Parameters.Add("@Name", SqlDbType.Char, 8).Value = textBox2.Text.Trim();
			if (radioButton1.Checked)
			{
				cmd.Parameters.Add("@Sex", SqlDbType.Bit).Value = true;
			}
			else if (radioButton2.Checked)
			{
				cmd.Parameters.Add("@Sex", SqlDbType.Bit).Value = false;
			}
			else
			{
				MessageBox.Show("请选择性别");
			}
			cmd.Parameters.Add("@Spec", SqlDbType.Char, 12).Value = comboBox1.Text;
			cmd.Parameters.Add("@Born", SqlDbType.Date).Value = textBox3.Text.Trim();
			cmd.Parameters.Add("@Detail", SqlDbType.NText).Value = textBox4.Text.Trim();
			if(FileNamePath != "")
			{
				FileStream fs = null;
				fs = new FileStream(FileNamePath, FileMode.Open, FileAccess.Read);
				MemoryStream mem = new MemoryStream();
				byte[] data1 = new byte[fs.Length];
				fs.Read(data1, 0, (int)fs.Length);
				cmd.Parameters.Add("@Photo", SqlDbType.Image);
				cmd.Parameters["@Photo"].Value = data1;
			}
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show ("保存成功！");
				this.tReaderTableAdapter.Fill(this.mBOOKDataSet.TReader);
			}
			catch(Exception ex)
			{
				MessageBox.Show("出错！" + ex.Message);
			}
			finally
			{
				conn.Close();
				FileNamePath = "";
			}

		}

		private void button5_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox3_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			openFileDialog.Filter = "jpg图片|*.jpg|gif图片|*.gif|所有文件(*.*)|*.*";
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				FileNamePath = openFileDialog.FileName;
				pictureBox1.Image = Image.FromFile(FileNamePath);
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if(textBox1.Text == "")
			{
				MessageBox.Show("请输入借书证号！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "Delete From [TReader] where [ReaderID] = @ReaderID";
			SqlCommand cmd = new SqlCommand(sqlStr,conn);
			cmd.Parameters.Add("@ReaderID", SqlDbType.Char, 8).Value = textBox1.Text.Trim();
			try
			{
				conn.Open();
				int a = cmd.ExecuteNonQuery();
				this.tReaderTableAdapter.Fill(this.mBOOKDataSet.TReader);
				if (a == 1)
				{
					MessageBox.Show("删除成功！");
				}
				else
				{
					MessageBox.Show("数据库中没有此读者！");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				conn.Close();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Trim() == "")
			{
				MessageBox.Show("请输入借书证号！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "update [TReader] set";
			if(textBox2.Text.Trim().ToString() != "")
			{
				sqlStr += "[Name]='" + textBox2.Text.Trim() + "',";
			}
			if (textBox3.Text.Trim() != "")
			{
				sqlStr += "[Born]='" + textBox3.Text.Trim() + "',";
			}
			if(FileNamePath != "")
			{
				sqlStr += "[Photo]=@Photo,";
			}
			if (textBox4.Text.Trim() != "")
			{
				sqlStr += "[Detail]='" + textBox4.Text.Trim() + "',";
			}
			sqlStr += "[Spec]='" + comboBox1.Text + "',[Sex]=@Sex";
			sqlStr += " where ReaderID='" + textBox1.Text.Trim() + "'";
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			if (radioButton1.Checked){
				cmd.Parameters.Add("@Sex", SqlDbType.Bit).Value = true;
			}
			else if(radioButton2.Checked)
			{
				cmd.Parameters.Add("@Sex", SqlDbType.Bit).Value = false;
			}
			else
			{
				MessageBox.Show("请选择性别！");
				return;
			}
			if(FileNamePath != "")
			{
				FileStream fs = new FileStream(FileNamePath, FileMode.Open, FileAccess.Read);
				MemoryStream ms = new MemoryStream();
				byte[] data1 = new byte[fs.Length];
				fs.Read(data1, 0, (int)fs.Length);
				cmd.Parameters.Add("@Photo", SqlDbType.VarBinary).Value = data1;
			}
			try
			{
				conn.Open();
				int yxh = cmd.ExecuteNonQuery();
				if(yxh == 1)
				{
					MessageBox.Show("修改成功！");
				}
				else
				{
					MessageBox.Show("数据库中没有此读者！");
				}
				this.tReaderTableAdapter.Fill(this.mBOOKDataSet.TReader);
			}
			catch(Exception ex)
			{
				MessageBox.Show("出错，没有完成读者的修改！" + ex.Message);
			}
			finally
			{
				conn.Close();
				FileNamePath = "";
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text.Trim() == "")
			{
				MessageBox.Show("请输入借书证号！");
				return;
			}
			SqlConnection conn = new SqlConnection(strcon);
			string sqlStr = "select [ReaderID],[Name],[Sex],[Spec],[Born],[Photo],[Num],[Detail] from [TReader] where [ReaderID]='" + textBox1.Text.Trim() + "'";
			SqlCommand cmd = new SqlCommand(sqlStr, conn);
			try
			{
				conn.Open();
				SqlDataReader sdr = cmd.ExecuteReader();
				MemoryStream mem = null;
				if (sdr.HasRows)
				{
					sdr.Read();
					textBox2.Text = sdr["Name"].ToString();
					textBox3.Text = sdr["Born"].ToString();
					comboBox1.Text = sdr["Spec"].ToString();
					label7.Text = sdr["Num"].ToString() + "本";
					bool sex = Convert.ToBoolean(sdr["Sex"]);
					if (sex)
					{
						radioButton1.Checked = true;
					}
					else
					{
						radioButton2.Checked = true;
					}
					if (this.pictureBox1.Image != null)
					{
						pictureBox1.Image.Dispose();
					}
					if (sdr["Photo"] != System.DBNull.Value)
					{
						byte[] data = (byte[])sdr["Photo"];
						mem = new MemoryStream(data);
						this.pictureBox1.Image = Image.FromStream(mem);
					}
					textBox4.Text = sdr["Detail"].ToString();
				}
				else
				{
					MessageBox.Show("没有此读者！");
				}
				if (mem != null)
				{
					mem.Close();
				}
				if (!sdr.IsClosed)
				{
					sdr.Close();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				if(conn.State == ConnectionState.Open)
				{
					conn.Close();
				}
			}
		}

		private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if(e.RowIndex >= 0)
			{
				textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			}
			button1_Click(null, null);
		}
	}
}
