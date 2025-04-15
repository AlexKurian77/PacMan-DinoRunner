using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameSelector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void onClick(object sender, EventArgs e)
        {
            addUser();
            Process.Start(@"C:\Users\alexa\source\repos\Overall Project\Dino Game\Dino Game\bin\Debug\Dino Game.exe",getname.Text.ToString());
        }

        private void OnClick(object sender, EventArgs e)
        {
            addUser();
            Process.Start(@"C:\Users\alexa\source\repos\Overall Project\Pac Man Game Project\Pac Man Game Project\bin\Debug\net8.0-windows\Pac Man Game Project.exe",getname.Text.ToString());
        }

        private void EnterName(object sender, EventArgs e)
        {

        }
        private async void addUser()
        {
            string url = "http://localhost:8080/api/addUser";

            var data = new { name = getname.Text};

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
