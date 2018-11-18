using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data.MySqlClient;

namespace MailToMail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        List<string> mails = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Format = .txt");            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader oku = new StreamReader(openFileDialog1.FileName);
                string satir = oku.ReadLine();
                while (satir != null)
                {
                    mails.Add(satir);
                    satir = oku.ReadLine();
                    label3.Text = mails.Count.ToString();
                }
            }
            label8.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= mails.Count; i++)
            {               
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(textBox2.Text);
                msg.To.Add(new MailAddress(mails[i].ToString()));
                msg.Subject = textBox4.Text;
                msg.Body = textBox1.Text;
                SmtpClient mySmtpClient = new SmtpClient();
                System.Net.NetworkCredential myCredential = new System.Net.NetworkCredential(textBox2.Text, textBox3.Text);
                mySmtpClient.Host = "smtp.gmail.com"; // host adresi ben default olarak gmail paylaşıyorum.
                mySmtpClient.Port = 587;          // smtp port no
                mySmtpClient.EnableSsl = true;
                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = myCredential;
                mySmtpClient.Send(msg);
                msg.Dispose();
                System.Threading.Thread.Sleep(int.Parse(textBox5.Text) + 000);
                label6.Text = i.ToString();
            }           
        }
        int time = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {            
            label7.Text = time.ToString();
            time++;
        }
    }
}
