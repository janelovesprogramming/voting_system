using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Voiting_System_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ServiceReference1.VotingSystemClient Service = new ServiceReference1.VotingSystemClient();

        private void button1_Click(object sender, EventArgs e)
        {
            Service.AddUser("{ Name: \"Иван\", PassportSeries: 222, PassportNumber: 23124, WhoGives: \"me\", WhenGives: \"01.01.2015\", isAdmin: true, Password: 123456, Token: \"\" }");
            MessageBox.Show("Запись добавлена!");

        }
    }
}
