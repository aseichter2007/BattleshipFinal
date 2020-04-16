using System;
using System.Transactions;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Game game = new Game();
                game.RunGame();
                //Player p1 = new Player();
                //int[] shot = new int[2] { 1, 1 };
               // p1.HitCheck(shot,p1);

                Console.WriteLine("enter for new game or exit to quit game");
            }
            while (Console.ReadLine() != "exit");
        }
    }
}
