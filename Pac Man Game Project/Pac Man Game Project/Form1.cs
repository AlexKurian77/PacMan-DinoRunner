using Newtonsoft.Json;
using System.IO.Ports;
using System.Text;

namespace Pac_Man_Game_Project
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        bool goup, godown, goleft, goright;
        bool noup, nodown, noleft, noright;
        List<PictureBox> walls = new List<PictureBox>();
        List<PictureBox> coins = new List<PictureBox>();
        int speed = 15;
        int score = 0;
        string username;
        Ghost red, yellow, blue, pink;
        List<Ghost> ghosts = new List<Ghost>();

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
                                        if (data == "left")
                                        {
                                            goright = godown = goup = false;
                                            noright = nodown = noup = false;
                                            goleft = true;
                                            pacman.Image = Properties.Resources.pacman_left;
                                        }
                                        else if (data == "right")
                                        {
                                            goleft = godown = goup = false;
                                            noleft = nodown = noup = false;
                                            goright = true;
                                            pacman.Image = Properties.Resources.pacman_right;
                                        }
                                        else if (data == "up")
                                        {
                                            goleft = godown = goright = false;
                                            noleft = nodown = noright = false;
                                            goup = true;
                                            pacman.Image = Properties.Resources.pacman_up;
                                        }
                                        else if (data == "down")
                                        {
                                            goleft = goright = goup = false;
                                            noleft = noright = noup = false;
                                            godown = true;
                                            pacman.Image = Properties.Resources.pacman_down;
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

        public Form1()
        {
            InitializeComponent();
            SetUp();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && !noleft)
            {
                goright = godown = goup = false;
                noright = nodown = noup = false;
                goleft = true;
                pacman.Image = Properties.Resources.pacman_left;
            }
            if((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && !noright)
            {
                goleft = godown = goup = false;
                noleft = nodown = noup = false;
                goright = true;
                pacman.Image = Properties.Resources.pacman_right;
            }
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && !noup)
            {
                goleft = godown = goright = false;
                noleft = nodown = noright = false;
                goup = true;
                pacman.Image = Properties.Resources.pacman_up;
            }
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && !nodown)
            {
                goleft = goright = goup = false;
                noleft = noright = noup = false;
                godown = true;
                pacman.Image = Properties.Resources.pacman_down;
            }
            e.SuppressKeyPress = true;
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            PlayerMovevements();
            foreach (PictureBox wall in walls)
            {
                CheckBoundaries(pacman, wall);
            }
            foreach (PictureBox coin in coins)
            {
                CollectingCoins(pacman, coin);
            }
            if(score == coins.Count())
            {
                GameOver("You win, Score: " + score);
                ShowCoins();
                score = 0;
            }
            red.GhostMovement(pacman);
            yellow.GhostMovement(pacman);
            blue.GhostMovement(pacman);
            pink.GhostMovement(pacman);
            foreach (Ghost ghost in ghosts)
            {
                GhostCollision(ghost, pacman, ghost.image);
            }
        }

        private void StartButton(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel1.Visible = false;

            goleft = goright = goup = godown = false;
            noleft = noright = noup = nodown = false;
            score = 0;

            //Ghost Bits
            currScore.Text = "Score: " + score;
            requestForMaxScore();
            serialPortRecognizer();

            red.image.Location = new Point(100, 100);
            blue.image.Location = new Point(678, 212);
            yellow.image.Location = new Point(218, 482);
            pink.image.Location = new Point(628, 552);

            timer1.Start();
        }
        private void SetUp()
        {
            foreach(Control c in this.Controls)
            {
                if(c is PictureBox && c.Tag == "wall")
                {
                    walls.Add((PictureBox)c);
                }
                if(c is PictureBox && c.Tag == "coin")
                {
                    coins.Add((PictureBox)c);
                }
            }

            red = new Ghost(this,Properties.Resources.arnold,100,100);
            ghosts.Add(red);
            blue = new Ghost(this,Properties.Resources.abrez,678,212);
            ghosts.Add(blue);
            yellow = new Ghost(this, Properties.Resources.alex, 218, 482);
            ghosts.Add(yellow);
            pink = new Ghost(this, Properties.Resources.aayan, 628, 552);
            ghosts.Add(pink);

        }
        
        private void PlayerMovevements()
        {
            if (goleft)
            {
                pacman.Left -= speed;
            }
            else if (goright)
            {
                pacman.Left += speed;
            }
            else if (godown)
            {
                pacman.Top += speed;
            }
            else if (goup)
            {
                pacman.Top -= speed;
            }


            if(pacman.Left < -30)
            {
                pacman.Left = this.ClientSize.Width - pacman.Width;
            }
            if(pacman.Left + pacman.Width > this.ClientSize.Width)
            {
                pacman.Left = -10;
            }
            if (pacman.Top < -30)
            {
                pacman.Top = this.ClientSize.Height - pacman.Height;
            }
            if (pacman.Top + pacman.Height > this.ClientSize.Height)
            {
                pacman.Top = -10;
            }
        }
        private void ShowCoins()
        {
            foreach(PictureBox coin in coins)
            {
                coin.Visible = true;
            }
        }
        private void CheckBoundaries(PictureBox pacman,PictureBox wall)
        {
            if (pacman.Bounds.IntersectsWith(wall.Bounds))
            {
                if (goleft)
                {
                    noleft = true;
                    goleft = false;
                    pacman.Left = wall.Right + 2;
                }
                if (goright)
                {
                    noright = true;
                    goright = false;
                    pacman.Left = wall.Left - pacman.Width - 2;
                }
                if (goup)
                {
                    noup = true;
                    goup = false;
                    pacman.Top = wall.Bottom + 2;
                }
                if (godown)
                {
                    nodown = true;
                    godown = false;
                    pacman.Top = wall.Top - pacman.Height - 2;
                }
            }
        }
        private void CollectingCoins(PictureBox pacman,PictureBox coin)
        {
            if (pacman.Bounds.IntersectsWith(coin.Bounds))
            {
                if (coin.Visible)
                {
                    coin.Visible = false;
                    score += 1;
                    currScore.Text = "Score: "+score;
                }
            }
        }
        private void GhostCollision(Ghost g,PictureBox pacman,PictureBox ghost)
        {
            if (pacman.Bounds.IntersectsWith(ghost.Bounds))
            {
                GameOver("You Died, Score: " + score);
                g.ChangeDirection();
            }
        }
        private void GameOver(string msg)
        {
            panel1.Visible = true;
            panel1.Enabled = true;
            timer1.Stop();
            pacman.Location = new Point(483, 341);
            label2.Text = msg;
            updateScore();
            ShowCoins();
            score = 0;
        }
        private async void updateScore()
        {
            string url = "http://localhost:8080/api/add";

            var data = new { score = score, game = "pacman", name = username };

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
            string apiUrl = "http://localhost:8080/api/scorePacman";

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
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
