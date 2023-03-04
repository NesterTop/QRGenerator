using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Data;
using System.Data.SqlClient;
using Area;

namespace QRGenerator
{
    public partial class Form1 : Form
    {
        DataTable tovar;
        SqlConnection connection;
        int counter;
        string connectionString = @"Data Source=DESKTOP-A9MP2FF\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            var reader = new SqlCommand("select * from tovar", connection).ExecuteReader();

            tovar = new DataTable();
            tovar.Load(reader);

            counter = 0;

            GetData(counter);
        }

        public void GetData(int counter)
        {
            label1.Text = tovar.Rows[counter][0].ToString();
            label3.Text = tovar.Rows[counter][1].ToString();
            pictureBox2.Image = Image.FromFile(tovar.Rows[counter][2].ToString());

            label6.Text = tovar.Rows[counter + 1][0].ToString();
            label4.Text = tovar.Rows[counter + 1][1].ToString();
            pictureBox3.Image = Image.FromFile(tovar.Rows[counter + 1][2].ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            pictureBox1.Image = encoder.Encode(textBox1.Text);

            using(SaveFileDialog fd = new SaveFileDialog())
            {
                if(fd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(fd.FileName);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(counter > 0)
            {
                counter -= 1;
            }
            GetData(counter);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(counter < tovar.Rows.Count - 2)
            {
                counter += 1;
            }
            GetData(counter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double a = double.Parse(textBox2.Text);
            double b = double.Parse(textBox3.Text);

            MyArea area = new MyArea();
            double result = area.GetRectangleArea(a, b);
            label2.Text = result.ToString();
        }
    }
}
