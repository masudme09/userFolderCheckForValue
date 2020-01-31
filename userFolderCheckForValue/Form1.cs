using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace userFolderCheckForValue
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            List<user> userList = new List<user>();
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); //To generate report
            MessageBox.Show(appPath);
            string searchPath =tBxFolderPath.Text;
            //string searchPath = @"E:\";
            foreach (string userFolder in Directory.GetDirectories(searchPath))
            {
                string username = userFolder.Split('\\')[userFolder.Split('\\').Length-1];
                string seaPath = userFolder + @"\E3D2\des-rep.pmldat";
                if(File.Exists(seaPath))
                {
                    string[] fileText = File.ReadAllLines(seaPath);
                    foreach(string line in fileText)
                    {
                        if(line.Contains("!!tmpGphRepOpt.arcTolerance"))
                        {
                            user user = new user();
                            user.username = username;
                            user.arcTolValue = line.Split('=')[line.Split('=').Length-1];
                            userList.Add(user);
                        }
                    }

                }               

            }

            //Write details to file
            List<string> userDetails= new List<string>();
            foreach(user us in userList)
            {
                userDetails.Add(us.username + ';' + us.arcTolValue);
            }

            if(userDetails.Count>0)
            {
                File.WriteAllLines(appPath + "\\userDetails.txt", userDetails);
                MessageBox.Show("File wrritten to program directory");
            }
            else
            {
                MessageBox.Show("Nothing found");
            }
            

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
