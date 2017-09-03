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
