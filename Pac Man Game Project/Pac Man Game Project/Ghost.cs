using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man_Game_Project
{
    internal class Ghost
    {
        int speed = 6;
        int xSpeed = 6;
        int ySpeed = 6;
        int maxHeight = 617;
        int maxWidth = 911;
        int minHeight = 92;
        int minWidth = 85;

        int change = 0;
        Random random = new Random();
        string[] directions = { "left", "right", "up", "down","seek"};
        string direction = "left";
        public PictureBox image = new PictureBox();

        public Ghost(Form game,Image img,int x,int y)
        {
            image.Image = img;
            image.SizeMode = PictureBoxSizeMode.StretchImage;
            image.Width = 50;
            image.Height = 50;
            image.BringToFront();
            image.Left = x;
            image.Top = y;

            game.Controls.Add(image);
        }
        public void GhostMovement(PictureBox pacman)
        {
            if(change > 0)
            {
                change--;
            }
            else
            {
                change = random.Next(50,80);
                direction = directions[random.Next(directions.Length)];
            }
            switch (direction)
            {
                case "left":
                    image.Left -= speed;
                    break;
                case "right":
                    image.Left += speed;
                    break;
                case "up":
                    image.Top -= speed;
                    break;
                case "down":
                    image.Top += speed;
                    break;
                case "seek":
                    if (image.Left > pacman.Left)
                    {
                        image.Left -= xSpeed;
                    }
                    if (image.Left < pacman.Left)
                    {
                        image.Left += xSpeed;
                    }
                    if (image.Top > pacman.Top)
                    {
                        image.Top -= ySpeed;
                    }
                    if (image.Top < pacman.Top)
                    {
                        image.Top += ySpeed;
                    }
                    break;
            }
            if (image.Left < minWidth)
            {
                direction = "right";
            }
            if (image.Left + image.Width > maxWidth)
            {
                direction = "left";
            }
            if (image.Top < minHeight)
            {
                direction = "down";
            }
            if (image.Top + image.Height > maxHeight)
            {
                direction = "up";
            }
        }


        public void ChangeDirection()
        {
            direction = directions[random.Next(direction.Length)];
        }
    }
}
