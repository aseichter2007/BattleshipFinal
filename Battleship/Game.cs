using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Battleship
{
    class Game
    {
        Player player1;
        Player player2;
        int side = 1;
        public Game()
        {
            Console.WriteLine("player one setup. Press enter when ready");
            Console.ReadLine();
            player1 = new Player();
            Console.Clear();
            Console.WriteLine("Player 2 setup, press enter when ready.");
            Console.ReadLine();
            player2 = new Player();
            Console.Clear();

        }
        public void RunGame()
        {
            do
            {
                PlayetOneTurn();
                ChangeSide();
                PlayerTwoTurn();
                ChangeSide();
            }
            while (player1.fleet.Count > 0 && player2.fleet.Count > 0);
            if (player1.fleet.Count > player2.fleet.Count)
            {
                Console.WriteLine(player1.name + " wins!");
            }
            else
            {
                Console.WriteLine(player2.name + " wins!");
            }
        }
        public void HitCheck(Player player1,Player player2)
        {
           player1.HitSet(player2);
        }
        public void PlayetOneTurn()
        {
            player1.gameboard.Readout(player1.gameboard.playerGameBoard);
            Console.WriteLine("this is your fleet status. Press enter when ready.");
            Console.ReadLine();
            Console.Clear();
            HitCheck(player1, player2);
            Console.Clear();
            player1.gameboard.Readout(player1.gameboard.opponentGameBoard);
            Console.WriteLine("this is your opponents board as you know it. Press enter to end turn.");

            Console.ReadLine(); 
        }
        public void PlayerTwoTurn()
        {
            player2.gameboard.Readout(player2.gameboard.playerGameBoard);
            Console.WriteLine("this is your fleet status. Press enter when ready.");
            Console.ReadLine();
            Console.Clear();
            HitCheck(player2, player1);
            Console.Clear();
            player2.gameboard.Readout(player2.gameboard.opponentGameBoard);
            Console.WriteLine("this is your opponents board as you know it. Press enter to end turn.");
            Console.ReadLine();
        }
        public void ChangeSide()
        {
            string name;
            Console.Clear();
            if (side == 1)
            {
                side = 2;
                name = player2.name;
            }
            else
            {
                side = 1;
                name = player1.name;
            }
            Console.WriteLine("it is now {0}'s turn. press enter when ready", name);
            Console.ReadLine();
        }
    }
    
}
