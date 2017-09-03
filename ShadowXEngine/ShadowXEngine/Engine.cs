using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Drawing2D;

namespace ShadowXEngine
{
    /// <summary>
    /// The 'GameCanvas' is a custom class that inherits the main windows form panel class. We edit the panel so that it can draw more smoothly and is double buffered
    /// </summary>
    class GameCanvas : Panel
    {
        public GameCanvas()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
            this.BorderStyle = BorderStyle.Fixed3D;
        }
    }
    /// <summary>
    /// The main Game Engine Core. This abstract class drives everything.
    /// </summary>
    abstract class Engine
    {
        //Create a new windows form
        private Form window;
        //Create our custom GameCanvas class
        private GameCanvas canvas;
        //Our screenSize var
        private Vector2 screenSize;
        //Our camera
        private Vector2 cameraPosition = new Vector2(0,0);
        //Our scenes background color
        private Color bg = Color.Coral;
        //All the gameobjects in the game are stored in the gameObjects list
        private static List<Object2D> gameObjects = new List<Object2D>();
        //All the font elements of our game
        private static List<TextObject> fontObjects = new List<TextObject>();
        //Our keyeventarg for our down keys
        KeyEventArgs keyEventDown = null;
        //Our keyeventarg for our up keys
        KeyEventArgs keyEventUp = null;
        //Safty to make sure nothing crashes
        bool RendererIsActive = false;
        //Init our Engine with screenSize and a title
        public Engine(Vector2 screenSize, string title)
        {
            //Call our abstract Awake() method
            Awake();
            //Assign our window to a new form
            window = new Form();
            //Assign our panel/canvas to a new GameCanvas
            canvas = new GameCanvas();
            //Assign our screensize to our private var
            this.screenSize = screenSize;
            window.FormBorderStyle = FormBorderStyle.Fixed3D;
            //Set screen size
            window.Size = new Size((int)screenSize.X, (int)screenSize.Y);
            //Set window title
            window.Text = title;
            //Set our window icon
            window.Icon = null;
            //Add our GameCanvas to the new windows form
            window.Controls.Add(canvas);
            //Add a new EventHandler for our Resize event
            window.Resize += Resize;
            //Set our game canvas size
            canvas.Size = new Size((int)screenSize.X, (int)screenSize.Y);
            //Add a new PaintEvent
            canvas.Paint += Render;
            //Add our KeyDown Event
            window.KeyDown += IsKeyPressed;
            //Add our KeyUp Event
            window.KeyUp += IsKeyUp;
            //Create our main refresh thread. This thread handles stuff like Refreshing the canvas and keys
            Thread t = new Thread(new ThreadStart(RefreshRenderer));
            //Start our thread
            t.Start();
            //Start our events
            Application.DoEvents();
            //Open our form   
            Application.Run(window);
            
        }
        //Our Awake() method will start as soon as the Engine class has been created
        protected abstract void Awake();
        //OnStartFrame() method will be called at the start of our paint event after we clear all frames before it
        protected abstract void OnStartFrame();
        //The Draw() method is called inbetween StartFrame and EndFrame
        protected abstract void Draw();
        //OnEndFrame() method is called after all our draw calls
        protected abstract void OnEndFrame();
        //KeyPressed will send out our keyevent so that we can detect keys in our main game file
        protected abstract void KeyPressed(KeyEventArgs e);
        //KeyRelease will send out our keyevent when our key is released
        protected abstract void KeyRelease(KeyEventArgs e);
        /// <summary>
        /// Will return a List Array of Object2D's
        /// </summary>
        /// <returns></returns>
        public static List<Object2D> GetAllGameObjects()
        {
            return gameObjects;
        }
        /// <summary>
        /// IsKeyPressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IsKeyPressed(object sender, KeyEventArgs e)
        {
            //Assign our global keyEvent
            keyEventDown = e;          
        }
        /// <summary>
        /// IsKeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void IsKeyUp(object sender, KeyEventArgs e)
        {
            //Make our keyEvent null
            //keyEvent = null;
            keyEventUp = e;
        }
        /// <summary>
        /// Resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Resize(object sender, EventArgs e)
        {
            canvas.Size = window.Size;
        }
        /// <summary>
        /// Set or Get our scene background color
        /// </summary>
        public Color Background
        {
            get
            {
                return bg;
            }
            set
            {
                bg = value;
            }
        }
        /// <summary>
        /// Our RefreshRenderer thread does the most important work. Updating our canvas and keys
        /// </summary>
        void RefreshRenderer()
        {
            while(true)
            {
                try
                {
                    if (RendererIsActive)
                    {

                        canvas.Invoke((MethodInvoker)delegate ()
                        {
                            //Refresh our game canvas
                            canvas.Refresh();

                        });
                        window.Invoke((MethodInvoker)delegate ()
                        {
                            //Call our key methods
                            if (keyEventDown != null)
                            {
                                KeyPressed(keyEventDown);
                                keyEventDown = null;
                            }
                            if(keyEventUp != null)
                            {
                                KeyRelease(keyEventUp);
                                keyEventUp = null;
                            }
                        });
                        //Sleep. Doing this will make the game less laggy. If there is 0 delay you will find issues when you have a large amount of text/2d objects
                        Thread.Sleep(1);
                    }
                }
                catch
                {
                    Console.WriteLine("Canvas can not be located. [ShadowXEngine Will Now Stop All Threads]");
                    
                    Console.Read();
                    break;
                }
            }
        }
        /// <summary>
        /// Create a new Object2D
        /// </summary>
        /// <param name="obj2d"></param>
        public static void CreateObject2D(Object2D obj2d)
        {
            gameObjects.Add(obj2d);
        }
        /// <summary>
        /// Create a new TextObject
        /// </summary>
        /// <param name="text"></param>
        public static void CreateTextObject(TextObject text)
        {
            fontObjects.Add(text);
        }
        /// <summary>
        /// Change the viewport position
        /// </summary>
        /// <param name="pos"></param>
        public void CameraPosition(Vector2 pos)
        {
            float bX = pos.X * canvas.Width;
            float bY = pos.Y * canvas.Height;
            cameraPosition = new Vector2(bX / 100,bY / 100);
        }
        /// <summary>
        /// Our main core of the engine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Render(object sender, PaintEventArgs e)
        {
            //Our start frame
            OnStartFrame();
            Graphics g = e.Graphics;
            //Clear everything that needs to be cleared / reset
            gameObjects.Clear();
            fontObjects.Clear();
            g.Clear(bg);
            //Set our camera position
            Matrix m = new Matrix();
            m.Translate(cameraPosition.X, cameraPosition.Y);
            g.Transform = m;
            //Call our draw method
            Draw();
            
            //Place all objects that are in our gameObjects list
            foreach(Object2D o in gameObjects)
            {
                switch(o.objectType)
                {
                    case Object2D.ObjectType.Image:
                        try
                        {
                            Bitmap bitmap = new Bitmap(o.ImageDir);
                            float bWidth = o.Scale.X * canvas.Height;
                            float bHeight = o.Scale.Y * canvas.Height;
                            float bX = o.Position.X * canvas.Width;
                            float bY = o.Position.Y * canvas.Height;
                            
                            g.DrawImage(bitmap,bX / 100, bY / 100,bWidth / 100,bHeight / 100);
                            
                        }
                        catch
                        {
                            Console.WriteLine("[ERROR][Object2D]["+o.Tag+"]["+o.ImageDir+"] - Directory can not be found. Have you set 'ImageDir'?");
                        }
                        break;
                    case Object2D.ObjectType.Rectangle:
                        float iWidth = o.Scale.X * canvas.Height;
                        float iHeight = o.Scale.Y * canvas.Height;
                        float iX = o.Position.X * canvas.Width;
                        float iY = o.Position.Y * canvas.Height;
                        g.FillRectangle(new SolidBrush(Color.Black),iX / 100, iY / 100, iWidth / 100, iHeight / 100);
                        break;
                    case Object2D.ObjectType.Sphere:
                        //This is yet to be added
                        break;
                }
            }
            //Place all text objects in our fontObjects list array
            foreach (TextObject t in fontObjects)
            {
                Font drawFont = new Font(t.Font, t.FontSize);
                float bX = t.Position.X * canvas.Width;
                float bY = t.Position.Y * canvas.Height;
                g.DrawString(t.Text, drawFont, new SolidBrush(t.FontColor), bX / 100, bY / 100);
            }
            //Call our end frame
            OnEndFrame();
            //Make sure we tell everything that we are still running
            RendererIsActive = true;
            //g.Dispose();
        }
    }
}
