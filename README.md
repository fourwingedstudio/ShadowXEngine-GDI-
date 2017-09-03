# ShadowXEngine-GDI-
The ShadowXEngine is my very first attempt at any kind of engine. It uses all default windows librarys. 
I started working on this as i wanted to start learning the basics about game engines now that i am pretty confident in what i am doing i will
be re-making the ShadowXEngine in OPENGL

# Documentation

Your game class must inherit from the 'Engine' class.
>class EmptyDemoGame : Engine  {
>   
>}

You will need to now give your game a few basic settings with in your game class.
>public EmptyDemoGame() : base(new Vector2(512,512), "EmptyDemoGame") {
>
>}

> : base(Window and Canvas size, Window / Game name)

Next we need to add all of our methods used to make our game. Your entire game class should look like our example below.

```sh
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShadowXEngine
{
    class EmptyDemoGame : Engine
    {
        public EmptyDemoGame() : base(new Vector2(512,512), "EmptyDemoGame")
        {

        }
        //Called once as soon as the game runs
        protected override void Awake()
        {
            throw new NotImplementedException();
        }
        //Called every draw call
        protected override void Draw()
        {
            throw new NotImplementedException();
        }
        //Called every key press
        protected override void KeyPressed(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        //Called every key release
        protected override void KeyRelease(KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
        //Called at the end of a draw call
        protected override void OnEndFrame()
        {
            throw new NotImplementedException();
        }
        //Called at the start of a draw call. Note that nothing can be drawn in this method
        protected override void OnStartFrame()
        {
            throw new NotImplementedException();
        }
    }
}
```
## Creating a new object. (All objects should be created inside of the draw call)
* Object2D test = new Object2D(position,scale,ObjectType, ImageDir, ObjectTag);
Adding a tag on our game objects will allow us to use collision so make sure you add tags for all objects.
> Object2D player = new Object2D(new Vector2(x,y), new Vector2(7, 12), Object2D.ObjectType.Image, "Assets/Images/player.png", "player");

## Creating a new TextObject. (All text objects should be created inside the draw call)
* TextObject test = new TextObject(position, font size, font name, text to render, color);
>TextObject text = new TextObject(new Vector2(x - 5, y - 10), 16, "Arial", "Score: " + score, Color.Red);

## Detecting Collision
To detect if 2 objects are touching we do the following
```sh
if (Physics2D.PlaceMeeting("player", "goldCoin"))
{
    //Do something cool
}
```
Basic movement collision can be down like this...
```sh
if (!Physics2D.PlaceMeeting("player", "ground"))
{
    lastPos = pl.Position;
}
else
{
    pl.Position.X = lastPos.X;
    pl.Position.Y = lastPos.Y;
    x = lastPos.X;
    y = lastPos.Y;
}
```
## Other things
Change the camera position
>CameraPosition(new Vector2(x,y));

Change the camera position
>CameraPosition(new Vector2(x,y));

To see if a key has been pressed or released we do the following inside of our KeyPressed / KeyReleased methods
>if (Input.GetKey(e, Keys.D))
>{
>
>}

To start the game you have created you must add the class to the Program.cs main method
> Game test = new Game();

Thats pretty much it. Everything else is left to your creativity. Please remember this is only using Winforms GDI+ so after around 40-50
game objects you will start to see HEAVY frame drop. The engine comes with a SAMPLE game that anyone can see the code and learn from that.

## Credits
Kenney (https://kenney.itch.io/) - Art
ShadowDragon / Laurence - Coder of the entire project and founder of Four Winged Studio

## LICENSE
You are allowed to use this code for personal use only. All assets used in this project MUST not be sold they are the property of Kenney
If you would like to use these assets commercially please purchase them at Kenneys ITCH page https://kenney.itch.io/
