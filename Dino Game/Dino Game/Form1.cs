using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Dino_Game
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        bool jumping = false;
        int jumpSpeed;
        int force = 12;
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver=false;
        string username;

        public Form1()
        {
            InitializeComponent();
            requestForMaxScore();
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1) // First argument is always the .exe name
            {
                username = args[1]; // Get the passed username
            }
            else
            {
                username = "";
            }
        }
        private void serialPortRecognizer()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    using (SerialPort serialPort = new SerialPort("COM8", 115200))
                    {
                        serialPort.DtrEnable = false; // Prevents Arduino from resetting
                        serialPort.RtsEnable = false;
                        serialPort.ReadTimeout = 5000; // Prevent infinite wait

                        try
                        {
                            serialPort.Open();
                            Console.WriteLine("Connected to Arduino!");

                            while (serialPort.IsOpen)
                            {
                                try
                                {
                                    string data = serialPort.ReadLine().Trim().ToLower();
                                    Console.WriteLine($"Received: {data}");

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        
                                        if (data == "up")
                                        {
                                            jumping = true;
                                        }

                                    });
                                }
                                catch (TimeoutException)
                                {
                                    Console.WriteLine("Serial read timeout, retrying...");
                                }
                                catch (IOException ex)
                                {
                                    Console.WriteLine($"I/O Error: {ex.Message}");
                                    break; // Break out of loop to reconnect
                                }
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            Console.WriteLine("Port is in use! Trying again in 2 seconds...");
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("Lost connection! Trying to reconnect...");
                        }
                        finally
                        {
                            if (serialPort.IsOpen)
                                serialPort.Close();
                        }
                    }

                    // Wait before reconnecting
                    Thread.Sleep(2000);
                }
            });
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void MainGameTimeEvent(object sender, EventArgs e)
        {
            trex.Top += jumpSpeed;
            txtScore.Text = "Score: " + score;

            if(jumping==true && force < 0)
            {
                jumping=false;
            }
            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if(trex.Top>272 && jumping == false)
            {
                force = 12;
                trex.Top = 273;
                jumpSpeed = 0;
            }

            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;
                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width+rand.Next(200,500)+(x.Width*15);
                        score++;
                    }
                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        textLabel.Text = "Press R to restart the game!";
                        isGameOver=true;
                        panel1.Visible = true;
                        updateScore();
                    }
                }
            }
            if (score > 5)
            {
                obstacleSpeed = 15;
            }
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode==Keys.Space || e.KeyCode == Keys.Up) && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (jumping == true)
            {
                jumping = false;
            }
            if(e.KeyCode==Keys.R && isGameOver == true)
            {
                GameReset();
            }
        }

        private void GameReset()
        {
            requestForMaxScore();
            panel1.Visible = false;
            force = 12;
            jumpSpeed = 0;
            jumping=false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            trex.Image=Properties.Resources.Aditya;
            isGameOver = false;
            trex.Top = 273;

            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width + rand.Next(500, 800)+(x.Width * 10);
                    x.Left = position;

                }
            }
            gameTimer.Start();

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            GameReset();
            serialPortRecognizer();

        }
        private async void updateScore()
        {
            string url = "http://localhost:8080/api/add";

            var data = new { score = score, game = "dino", name = username };

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);
                string result = await response.Content.ReadAsStringAsync();
            }
        }
        private async void requestForMaxScore()
        {
            string apiUrl = "http://localhost:8080/api/scoreDino";

            try
            {
                string response = await GetApiResponse(apiUrl);
                maxScore.Text = "Max Score: " + response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching max score: {ex.Message}");
                maxScore.Text = "Max Score: 0";
            }
        }

        private async Task<string> GetApiResponse(string url)
        {
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
