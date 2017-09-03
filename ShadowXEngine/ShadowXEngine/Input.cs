using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace ShadowXEngine
{
    class Input
    {
        
        public static bool GetKey(KeyEventArgs e, Keys key)
        {
            if(e.KeyCode == key)
            {
                return true;
            }
            return false;
        }
    }
}
