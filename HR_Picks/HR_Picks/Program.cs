using HR_Picks.Bot;
using System;

namespace HR_Picks
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new HRPicksBot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
