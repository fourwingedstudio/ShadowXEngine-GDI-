using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
/// <summary>
/// Test demo for the ShadowXEngine using GDI+
/// Please note that all assets that come with the demo are not for sale other then on Kenneys offical itch.io page. You are only allowed to use these assets personaly unless you purchase them via his page
/// </summary>
namespace ShadowXEngine
{
    class TestDemoGame : Engine
    {

        public TestDemoGame() : base(new Vector2(512,512), "TestDemoGame")
        {

        }

        protected override void Awake()
        {
            Console.WriteLine("Awake()");
            Background = Color.LightBlue;
        }
        float x;
        float y;
        bool left = false;
        bool right = false;
        bool up = false;
        bool down = false;
        bool isJumping = false;
        int score = 0;
        Vector2 lastPos;
        bool[] stars = { true,true,true,true };
        float jumpHeight = 0;
        float maxJumpHeight = 15;
        protected override void Draw()
        {
            
            Object2D pl = new Object2D(new Vector2(x,y), new Vector2(7, 12), Object2D.ObjectType.Image, "Assets/Images/player.png", "player");
            TextObject scoreText = new TextObject(new Vector2(x - 5, y - 10), 16, "Arial", "Score: " + score, Color.Red);
            TextObject jt = new TextObject(new Vector2(x - 5, y - 15), 16, "Arial", "Jump Height: " + jumpHeight, Color.Red);
            float j = 0;
           
            for (int i = 0; i < 30; i++)
            {
                
                if(i == 5 || i == 6)
                {
                    Object2D ground = new Object2D(new Vector2(j, 45), new Vector2(15, 15), Object2D.ObjectType.Image, "Assets/Images/tile1.png", "ground");
                }
                else
                {
                    Object2D ground = new Object2D(new Vector2(j, 60), new Vector2(15, 15), Object2D.ObjectType.Image, "Assets/Images/tile1.png", "ground");
                }
                Object2D star = null;
                switch (i)
                {
                    case 3:
                        if(stars[0] == true)
                            star = new Object2D(new Vector2(j, 50), new Vector2(5, 5), Object2D.ObjectType.Image, "Assets/Images/star.png", "star1");
                        break;
                    case 4:
                        if (stars[1] == true)
                            star = new Object2D(new Vector2(j, 50), new Vector2(5, 5), Object2D.ObjectType.Image, "Assets/Images/star.png", "star2");
                        break;
                    case 7:
                        if (stars[2] == true)
                            star = new Object2D(new Vector2(j, 50), new Vector2(5, 5), Object2D.ObjectType.Image, "Assets/Images/star.png", "star3");
                        break;
                    case 20:
                        if (stars[3] == true)
                            star = new Object2D(new Vector2(j, 50), new Vector2(5, 5), Object2D.ObjectType.Image, "Assets/Images/star.png", "star4");
                        break;
                        
                }
                j += 14.5f;
            }
            if (Physics2D.PlaceMeeting("player", "star1"))
            {
                stars[0] = false;
                score++;
            }
            if (Physics2D.PlaceMeeting("player", "star2"))
            {
                stars[1] = false;
                score++;
            }
            if (Physics2D.PlaceMeeting("player", "star3"))
            {
                stars[2] = false;
                score++;
            }
            if (Physics2D.PlaceMeeting("player", "star4"))
            {
                stars[3] = false;
                score++;
            }

            if (!Physics2D.PlaceMeeting("player", "ground"))
            {
                lastPos = pl.Position;
                CameraPosition(new Vector2(-x + 40, -y + 40));
            }
            else
            {
                
                pl.Position.X = lastPos.X;
                pl.Position.Y = lastPos.Y;
                x = lastPos.X;
                y = lastPos.Y;
                
            }

            if (right == true)
            {
                x++;
            }
            if (left == true)
            {
                x--;
            }
            if (up == true)
            {
                //y--;
                //camY-= 5;
            }
            if (down == true)
            {
                //y++;
                //camY += 5;
            }
            if (Physics2D.PlaceMeeting("player", "ground", 0, -1f))
            {
                jumpHeight = 0;
            }
            if (!Physics2D.PlaceMeeting("player", "ground",0,-1f))
            {
                if (isJumping == false)
                {
                    y++;
                }
            }
            else
            {
                //Console.WriteLine("Gravity stopped");
            }
            if (isJumping == true)
            {
                if (jumpHeight <= maxJumpHeight)
                {
                    y -= 2;
                    jumpHeight++;
                }
            }

        }


        protected override void KeyPressed(KeyEventArgs e)
        {
            if (Input.GetKey(e, Keys.D))
            {
                right = true;
            }
            if (Input.GetKey(e, Keys.A))
            {
                left = true;
            }
            if (Input.GetKey(e, Keys.W))
            {
                up = true;
            }
            if (Input.GetKey(e, Keys.S))
            {
                down = true;
            }
            if (Input.GetKey(e, Keys.Space))
            {
                if (jumpHeight <= maxJumpHeight)
                {
                    isJumping = true;
                }
                else
                {
                    isJumping = false;
                }
            }
        }

        protected override void KeyRelease(KeyEventArgs e)
        {
            if (Input.GetKey(e, Keys.D))
            {
                right = false;
            }
            if (Input.GetKey(e, Keys.A))
            {
                left = false;
            }
            if (Input.GetKey(e, Keys.W))
            {
                up = false;
            }
            if (Input.GetKey(e, Keys.S))
            {
                down = false;
            }
            if (Input.GetKey(e, Keys.Space))
            {
                isJumping = false;
            }
        }

        protected override void OnEndFrame()
        {
            //Nothing can be drawn in the end frame method
        }

        protected override void OnStartFrame()
        {

        }
    }
}
