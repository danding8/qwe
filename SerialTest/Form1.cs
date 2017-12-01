using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SerialTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)//窗口创建初始化函数
        {
            string str;//用来临时存储i大写的十六进制格式字符串

            for (int i = 0; i < 256; i++)//256个
            {
                str = i.ToString("x").ToUpper();//ToString("x")是将数字转转换为16进制字符串，ToUpper是将字符串所有字符大写
                //comboBox1.Items.Add("0x" + (str.Length == 1 ? "0" + str : str));
                if (str.Length == 1)
                    str = "0" + str;//如果是一位的（0xA），此时为了对齐，在数据前加一个字符“0”（0x0A）
                comboBox1.Items.Add("0x"+ str);//统一添加"0x"
            }
            comboBox1.Text = "0X00";//初始值
        }

        private void button1_Click(object sender, EventArgs e)//按键单击事件
        {
            string data = comboBox1.Text;//存储当前下拉框的内容
            string convertdata = data.Substring(2, 2);//把字符分开
            byte[] buffer = new byte[1];//数据一个字节就够用了

            buffer[0] = Convert.ToByte(convertdata, 16);
            //将字符串转化为byte型变量（byte相当于单片机中的unsigned char（0-255））

            try//防止出错
            {
                serialPort1.Open();
                serialPort1.Write(buffer, 0, 1);
                serialPort1.Close();
            }
            catch {//如果出错就执行此块代码
                if (serialPort1.IsOpen)
                    serialPort1.Close();//如果是写数据时出错，此时窗口状态为开，就应关闭串口，防止下次不能使用，串口是不能重复打开和关闭的
                MessageBox.Show("端口错误","错误");
            }
        }
    }
}
