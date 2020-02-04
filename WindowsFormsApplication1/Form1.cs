using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        
        private Int32 flagNum = 0;
        private Int32 dValue;
        private bool Flag = true;
        private bool voice_flag = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe","-s");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            for (int i = 0; i < 24; i++)//时
            {
                comboBox1.Items.Add(i.ToString());
            }
            for (int i = 0; i < 60; i++)//分
            {
                comboBox2.Items.Add(i.ToString());
            }
            for (int i = 0; i < 60; i++)//秒
            {
                comboBox3.Items.Add(i.ToString());
            }
            timer3.Stop();
            timer3.Interval = 500;
            comboBox1.Text = System.DateTime.Now.Hour.ToString();
            comboBox2.Text = System.DateTime.Now.Minute.ToString();
            comboBox3.Text = System.DateTime.Now.Second.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.textBox1.Text = System.DateTime.Now.ToString();//显示系统时间
        }

        private void button4_Click(object sender, EventArgs e)
        {
             Int32 timingHour;
             Int32 timingMinute;
             Int32 timingSecond;
             Int32 systemHour;
             Int32 systemMinute;
             Int32 systemSecond;
            if ((comboBox1.Text == "") && (comboBox2.Text == "") && (comboBox3.Text == ""))
            {
                MessageBox.Show("还没输入时间呢，兄dei  ! ! !","提示");
                return;
            }
            systemHour = System.DateTime.Now.Hour;
            systemMinute = System.DateTime.Now.Minute;
            systemSecond = System.DateTime.Now.Second;
            timingHour = Convert.ToInt16(comboBox1.Text);
            timingMinute = Convert.ToInt16(comboBox2.Text);
            timingSecond = Convert.ToInt16(comboBox3.Text);
            dValue = timingHour * 3600 + timingMinute * 60 + timingSecond - systemHour * 3600 - systemMinute * 60 - systemSecond;
            if(dValue<=0)
            {
                dValue = 24 * 3600 + dValue;
            }
            textBox2.Text = "";
            textBox2.Text = (dValue-flagNum) / 3600 + "时";
            textBox2.Text += (dValue - flagNum) % 3600 / 60 + "分";
            textBox2.Text += (dValue - flagNum) % 3600 % 60 + "秒";
            if (radioButton1.Checked)
                label6.Text = "关机";
            else if (radioButton2.Checked)
                label6.Text = "重启";
            else if (radioButton3.Checked)
                label6.Text = "睡眠";
            else if (radioButton4.Checked)
                label6.Text = "闹钟";
            timer2.Interval = 1000;
            if (Flag)
            {
                timer2.Start();
                Flag = false;
                button4.Text = "后悔了吧\n快点关";
                flagNum = 0;
            }
            else
            {
                timer2.Stop();
                Flag = true;
                textBox2.Text = "";
                timer3.Stop();
                button4.Text = "启动";
                flagNum = 0;
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            flagNum++;
            textBox2.Text = "";
            textBox2.Text = (dValue - flagNum) / 3600 + "时";
            textBox2.Text += (dValue - flagNum) % 3600 / 60 + "分";
            textBox2.Text += (dValue - flagNum) % 3600 % 60 + "秒";
            try
            {
                if (flagNum >= dValue)
            {
                timer2.Stop();
                flagNum = 0;
                //MessageBox.Show("时间到了！！！","提示");
                if (radioButton1.Checked)
                    Process.Start("shutdown.exe", "-s");
                else if (radioButton2.Checked)
                    Process.Start("shutdown.exe", "-r");
                else if (radioButton3.Checked)
                    Process.Start("shutdown.exe", "-h");
                else if (radioButton4.Checked)
                    timer3.Start();
                else
                    MessageBox.Show("您没有赋予执行任务", "提示");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("时间太大了，不支持","提示");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe", "-h");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown.exe", "-r");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }



    }
}
