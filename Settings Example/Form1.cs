using System;
using System.Windows.Forms;
using TSettings;
using TSettings.Encryptions;

namespace Settings_Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Settings.Encryption = new DesEncrpytion("ABCDEFGH", "ABCDEFGH");

            if (!Settings.Default.Get("Class", false))
            {
                yourage.Value = Settings.Default.Get<decimal>("age", 0);
                firstname.Text = Settings.Default.Get("name", "Name not found");
            }
            else
            {
                Userinfo info = Settings.Default.Get<Userinfo>("Info");

                yourage.Value = info.Age;
                firstname.Text = info.Name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.Set("Class", false);
            Settings.Default.Set("age", yourage.Value);
            Settings.Default.Set("name", firstname.Text);
            Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.Default.Set("Class", true);
            var user = new Userinfo((int) yourage.Value, firstname.Text);
            Settings.Default.Set("Info", user);
            Settings.Default.Save();
        }
    }
}
