using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Transactions;

namespace Battleship
{
    class Player
    {
        public string name = "";
        public Gameboard gameboard;
        
        public List<Ship> fleet;

        public Player()
        {
            gameboard = new Gameboard();

            PlayerSetup();
            fleet = new List<Ship>() { gameboard.battleship, gameboard.carrier,gameboard.submarine, gameboard.destroyer };
        }
        public bool HitSet(Player opponent)
        {
            bool output = false;
            int[] shot = new int[2];
            string[] shotset = new string[2];
            bool test = false;
            do
            {
                gameboard.Readout(gameboard.opponentGameBoard);
                Console.WriteLine("enter your strike coordinate.  'left index','top index'");
                string coordinate = Console.ReadLine();
                if (coordinate.Contains("a") || coordinate.Contains("z") || coordinate.Contains("y") || coordinate.Contains("x") || coordinate.Contains("w") || coordinate.Contains("v") || coordinate.Contains("u") || coordinate.Contains("t") || coordinate.Contains("s") || coordinate.Contains("r") || coordinate.Contains("q") || coordinate.Contains("p") || coordinate.Contains("o") || coordinate.Contains("n") || coordinate.Contains("m") || coordinate.Contains("l") || coordinate.Contains("k") || coordinate.Contains("j") || coordinate.Contains("i") || coordinate.Contains("h") || coordinate.Contains("g") || coordinate.Contains("f") || coordinate.Contains("e") || coordinate.Contains("d") || coordinate.Contains("c") || coordinate.Contains("b"))
                {

                }
                else if (coordinate.Length < 6)
                {
                    shot = ParseCoordinate(coordinate);
                    if (shot[0] > 0 && shot[0] < 21 && shot[1] > 0 && shot[1] < 21)
                    {
                    test = true;
                    output = HitCheck(shot, opponent);
                    }
                }                
                else
                {
                    Console.WriteLine("output of bounds. try again");
                    Console.WriteLine();

                }
            }
            while (test == false);

            return output;
        }
        public bool HitCheck(int [] shot,Player opponent)
        {
            bool output = false;
            if (opponent.gameboard.playerGameBoard[shot[0],shot[1]] == gameboard.ship)
            {
                gameboard.opponentGameBoard[shot[0], shot[1]] = gameboard.hitship;
                opponent.gameboard.playerGameBoard[shot[0], shot[1]] = gameboard.hitship;
                Console.WriteLine("HIT!! press enter.");
                Console.ReadLine();
                string sunk = ShipHit(shot, opponent);
                if (sunk != "")
                {
                    Console.WriteLine("you sunk your opponents {0}! press enter to continue.", sunk);
                    Console.ReadLine();
                }
            }
            else if (opponent.gameboard.playerGameBoard[shot[0], shot[1]] == gameboard.empty)
            {
                output = true;
                gameboard.opponentGameBoard[shot[0], shot[1]] = gameboard.miss;
                opponent.gameboard.playerGameBoard[shot[0], shot[1]] = gameboard.miss;
                Console.WriteLine("miss. press enter.");
                Console.ReadLine();

            }
            else if (opponent.gameboard.playerGameBoard[shot[0], shot[1]] == gameboard.hitship)
            {
                Console.WriteLine("that segment is already engulfed in flames. press enter.");
                Console.ReadLine();
            }
            else if (opponent.gameboard.playerGameBoard[shot[0], shot[1]] == gameboard.miss)
            {
                Console.WriteLine("you already missed there. press enter.");
                Console.ReadLine();
            }

            return output;
        }

        public string ShipHit(int[] coordinate, Player opponent)
        {
            bool breaker = false;
            string output = "";
            for (int i = 0; i < opponent.fleet.Count; i++)
            {
                for (int j = 0; j < opponent.fleet[i].placement.Count; j++)
                {
                    if (opponent.fleet[i].placement[j][0] == coordinate[0] && opponent.fleet[i].placement[j][1] == coordinate[1])
                    {
                        opponent.fleet[i].placement.RemoveAt(j);
                        if (opponent.fleet[i].placement.Count < 1)
                        {
                            output = opponent.fleet[i].name;
                            opponent.fleet.RemoveAt(i);
                        }
                        breaker = true;
                        break;
                    }
                   if (breaker)
                    {
                        break;
                    } 
                }
                if (breaker)
                {
                    break;
                }

            }
            return output;
        }
        public void PlayerSetup()
        {
            Console.WriteLine("Enter player name");
            name = Console.ReadLine();
            bool check = false;
            do
            {
                PlaceShip("battleship", 5);
                PlaceShip("carrier", 4);
                PlaceShip("submarine", 3);
                PlaceShip("destroyer", 2);
                gameboard.Readout(gameboard.playerGameBoard);
                Console.WriteLine("are the ships where you want them?");
                string setup = Console.ReadLine();
                if(setup == "yes" || setup=="y"||setup == "ok")
                {
                    check = true;
                }
                else
                {
                    gameboard = new Gameboard();
                }
            }
            while (check ==false);

            
        }

        public void PlaceShip(string ship, int length)
        {
            bool test = false;
            string direction = "";
            while (test == false)
            {
                gameboard.Readout(gameboard.playerGameBoard);
                Console.WriteLine("Place your {0}, length {1}.",ship, length);
                Console.WriteLine("enter starting coordinates as numbers 1-20: vertical,horizontal");
                string coordinate = Console.ReadLine();
                Console.WriteLine("enter direction from start coordinate. up down left right");
                direction = Console.ReadLine();
                if (direction == "left"||direction=="right" ||direction == "up" || direction == "down")
                {
                    if (coordinate.Contains("a") || coordinate.Contains("z") || coordinate.Contains("y") || coordinate.Contains("x") || coordinate.Contains("w") || coordinate.Contains("v") || coordinate.Contains("u") || coordinate.Contains("t") || coordinate.Contains("s") || coordinate.Contains("r") || coordinate.Contains("q") || coordinate.Contains("p") || coordinate.Contains("o") || coordinate.Contains("n") || coordinate.Contains("m") || coordinate.Contains("l") || coordinate.Contains("k") || coordinate.Contains("j") || coordinate.Contains("i") || coordinate.Contains("h") || coordinate.Contains("g") || coordinate.Contains("f") || coordinate.Contains("e") || coordinate.Contains("d") || coordinate.Contains("c") || coordinate.Contains("b"))
                    {

                    }                    
                    else if (coordinate.Length<6)                      
                    {
                        int[] start = ParseCoordinate(coordinate);
                        if (start[0] > 0 && start[0] < 21 && start[1] > 0 && start[1] < 21)
                        {
                            test = gameboard.TestShip(start, length, direction);
                        }
                    }
                }
                if (test == false)
                {
                    Console.WriteLine("out of bounds, enter to try again");
                    Console.ReadLine();
                }
            }
        }
        public int [] ParseCoordinate(string coordinate)
        {
            int count = coordinate.Length;
            int x = 0;
            int y = 0;
            string buffer = "";
            foreach (char number in coordinate)
            {
                count--;
                if (number== ',' || number == ' ')
                {
                    x = int.Parse(buffer);
                    buffer = "";
                }
                else if(count == 0)
                {
                    buffer += number;
                    y = int.Parse(buffer);
                }
                else
                {
                    buffer += number;
                }
            }
            int[] output = new int[2] { x, y };
            return output;
        }
    }
}
