using System.Collections;
using System.Windows.Forms;

namespace Snake_Dekhtiarenko
{
    public static class Input
    {

        private static Hashtable keys = new Hashtable();

        public static void ChangeState(Keys key, bool state)
        {
            keys[key] = state;
        }

        public static bool Press(Keys key)
        {
            if (keys[key] == null)
                keys[key] = false;
            return (bool) keys[key];
        }
    }
}
