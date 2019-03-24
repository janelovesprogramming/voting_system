using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VOTING_SYSTEM_CLIENT.VOTING_SERVER;
using Newtonsoft.Json;

namespace VOTING_SYSTEM_CLIENT
{
    public partial class Form1 : Form
    {
        VotingSystemClient client;

        public Form1()
        {
            InitializeComponent();
            client = new VotingSystemClient();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            object user = null;
            string stringUser = null;
            button1.Enabled = false;

            if (textBox1.Text != "" && textBox2.Text != "")
            {
                user = new { Name = textBox2.Text, Password = textBox1.Text };
            }
            else
            {
                MessageBox.Show("Введите логин и пароль!");
            }

            textBox1.Text = "";
            textBox2.Text = "";

            stringUser = JsonConvert.SerializeObject(user);

            Login(stringUser);
            button1.Enabled = true;
        }

        public async void Login(string userString)
        {
            string res = await client.LoginAsync(userString);

            if (res != null)
            {
                AuthorizationToken.Token = res;

                Visible = false;
                MainPage f = new MainPage();
                if (f != null)
                {
                    f.Owner = this;
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не сущетвует! Логи или пароль были не верно введены!",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    public static class AuthorizationToken
    {
        public static string Token { get; set; }
    }
}
