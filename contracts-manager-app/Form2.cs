using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace contracts_manager_app
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void registOrUpdate_Click(object sender, EventArgs e)
        {

        }

        public void LabelChanger(string buttonName)
        {
            registOrUpdate.Text = buttonName;
        }

        /// <summary>
        /// テキストボックスを事前に入力状態にしておくためのメソッド
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tel"></param>
        /// <param name="address"></param>
        /// <param name="remark"></param>
        public void TextBoxRegester(string name, string tel, string address, string remark)
        {
            nameBox.Text = name;
            telBox.Text = tel;
            addressBox.Text = address;
            remarkBox.Text = remark;
        }
    }
}
