using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.utilities
{
    //Id I was doing things correctly i would probably create this
    //as a service and inject it. but i will prob delete this before i am done. 
    public static class utils
    {
        public static void DebugMsg(string str)
        {
            Console.BackgroundColor  = ConsoleColor.Blue;
            System.Console.WriteLine(str);
            Console.BackgroundColor  = ConsoleColor.Black;
        }
    }
}