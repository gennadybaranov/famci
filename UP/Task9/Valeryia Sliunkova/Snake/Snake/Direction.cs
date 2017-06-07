using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Snake
{
    public static class Direction
    {
        private static Hashtable keys = new Hashtable();

        public static void Change(Keys key, bool state)
        {
            keys[key] = state;
        }

        public static bool Press(Keys key)
        {
            if (keys[key] == null)
                keys[key] = false;
            return (bool)keys[key];
        }
    }
}
