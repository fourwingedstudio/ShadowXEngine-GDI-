using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ShadowXEngine
{
    class Physics2D
    {
        /// <summary>
        /// This will check a if a collision has occurred between 2 objects
        /// </summary>
        /// <param name="tagA">Object2D Tag for your main object</param>
        /// <param name="tagB">Object2D Tag for your second object</param>
        /// <returns></returns>
        public static bool PlaceMeeting(string tagA, string tagB)
        {
            List<Object2D> o = Engine.GetAllGameObjects();
            List<Object2D> a = new List<Object2D>();
            List<Object2D> b = new List<Object2D>();
            for (int i = 0; i < o.Count; i++)
            {
                if (o[i].Tag == tagA)
                {
                    a.Add(o[i]);
                }
                if (o[i].Tag == tagB)
                {
                    b.Add(o[i]);
                }
            }
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if ((a[i].Position.X <= b[j].Position.X + b[j].Scale.X) && (a[i].Position.Y <= b[j].Position.Y + b[j].Scale.Y) && (a[i].Position.X + a[i].Scale.X >= b[j].Position.X) && (a[i].Position.Y + a[i].Scale.Y >= b[j].Position.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// This will check a if a collision has occurred between 2 objects using the offsets given
        /// </summary>
        /// <param name="tagA">Object2D Tag for your main object</param>
        /// <param name="tagB">Object2D Tag for your second object</param>
        /// <returns></returns>
        public static bool PlaceMeeting(string tagA, string tagB, float xOffset, float yOffset)
        {
            List<Object2D> o = Engine.GetAllGameObjects();
            List<Object2D> a = new List<Object2D>();
            List<Object2D> b = new List<Object2D>();
            for (int i = 0; i < o.Count; i++)
            {
                if (o[i].Tag == tagA)
                {
                    a.Add(o[i]);
                }
                if (o[i].Tag == tagB)
                {
                    b.Add(o[i]);
                }
            }
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if ((a[i].Position.X + xOffset <= b[j].Position.X + b[j].Scale.X) && (a[i].Position.Y + yOffset <= b[j].Position.Y + b[j].Scale.Y) && (a[i].Position.X + a[i].Scale.X >= b[j].Position.X + xOffset) && (a[i].Position.Y + a[i].Scale.Y >= b[j].Position.Y + yOffset))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool PlaceMeeting(string tagA, string tagB, Vector2 zoneScale)
        {
            List<Object2D> o = Engine.GetAllGameObjects();
            List<Object2D> a = new List<Object2D>();
            List<Object2D> b = new List<Object2D>();
            for (int i = 0; i < o.Count; i++)
            {
                if (o[i].Tag == tagA)
                {
                    a.Add(o[i]);
                }
                if (o[i].Tag == tagB)
                {
                    b.Add(o[i]);
                }
            }
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if ((a[i].Position.X <= b[j].Position.X + zoneScale.X) && (a[i].Position.Y <= b[j].Position.Y + zoneScale.Y) && (a[i].Position.X + zoneScale.X >= b[j].Position.X) && (a[i].Position.Y + zoneScale.Y >= b[j].Position.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }
}
