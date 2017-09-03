using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowXEngine
{
    class Object2D
    {
        public enum ObjectType
        {
            Rectangle,
            Sphere,
            Image
        }

        private ObjectType oType;

        private Vector2 position;
        private Vector2 scale;
        private string tag;
        private string imagedir;


        public Object2D(Vector2 position,Vector2 scale, ObjectType oType,string imagedir, string tag)
        {
            this.position = position;
            this.scale = scale;
            this.tag = tag;
            this.oType = oType;
            this.imagedir = imagedir;
            Engine.CreateObject2D(this);
        }

        public string ImageDir
        {
            get
            {
                return imagedir;
            }
            set
            {
                imagedir = value;
            }
        }
        public string Tag
        {
            get
            {
                return tag;
            }

        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
        }
        public ObjectType objectType
        {
            get
            {
                return oType;
            }
        }
    }
}
