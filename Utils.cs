using System;
using System.Collections.Generic;
using System.Text;

namespace EastfrisianRogue
{
    class Utils
    {
        public static void ClearConsoleLine(int line)
        {            
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, line);
        }
    }
}
