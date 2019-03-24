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
    public partial class MainPage : Form
    {
        VotingSystemClient client;

        public MainPage()
        {
            InitializeComponent();
            client = new VotingSystemClient();
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 owner = this.Owner as Form1;
            owner.Visible = true;
            AuthorizationToken.Token = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = JsonConvert.SerializeObject(new { Voting = 1, User = 1 });
            if (AuthorizationToken.Token != null)
            {
                label1.Text = client.GetVoicesCountInVoting(data, AuthorizationToken.Token);
            }
        }
    }
}
