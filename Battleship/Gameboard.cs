using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Gameboard
    {
        public string empty = "░";
        public string ship = "█";
        public string hitship = "◘";
        public string miss = "֍";
        public string [,] playerGameBoard;
        public string[,] opponentGameBoard;
        public Battleship battleship;
        public Carrier carrier;
        public Submarine submarine;
        public Destroyer destroyer;

        public Gameboard()
        {
            playerGameBoard = new string[21, 21];
            opponentGameBoard = new string[21, 21];
            PopulateEmptyBoard(opponentGameBoard);
            PopulateEmptyBoard(playerGameBoard);
            destroyer = new Destroyer();
            submarine = new Submarine();
            carrier = new Carrier();
            battleship = new Battleship();

        }
        public void chooseship(int[] coordinate, int length)
        {
            switch (length)
            {
                case 5:
                    battleship.Shipyard(coordinate);
                    break;
                case 4:
                    carrier.Shipyard(coordinate);
                    break;
                case 3:
                    submarine.Shipyard(coordinate);
                    break;
                case 2:
                    destroyer.Shipyard(coordinate);
                    break;
            }
        }
        public bool PlaceShipCheck(int[] start,int length,string direction)
        {
            bool output = true;
            {
                if (direction == "down")
                {
                    for (int i = start[0]; i <= start[0] + length; i++)
                    {
                        if (playerGameBoard[i, start[1]] == ship)
                        {
                            output = false;
                        }
                    }
                }
                else if (direction == "up")
                {
                    for (int i = start[0]; i >= start[0] - length; i--)
                    {
                        if (playerGameBoard[i, start[1]] == ship)
                        {
                            output = false;
                        }
                    }
                }
                else if (direction == "left")
                {
                    for (int i = start[1]; i >= start[1] - length; i--)
                    {
                        if (playerGameBoard[start[0], i] == ship)
                        {
                            output = false;
                        }
                    }
                }
                else if (direction == "right")
                {
                    for (int i = start[1]; i <= start[1] + length; i++)
                    {
                        if(playerGameBoard[start[0], i] == ship)
                        {
                            output = false;
                        }
                    }
                }
            }
            return output;
        }       
        public void PopulateEmptyBoard(string [,] gameboardin)
        {
            string[,] gameboard = gameboardin;
            for (int i = 0; i<21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    gameboard[i, j] = empty;
                }             
            }
            SetEdge(gameboard);
        }
        public void SetEdge(string [,] gameboardin)
        {
            string[,] gameboard= gameboardin;
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (i == 0)
                    {
                        if (j < 10)
                        {
                            gameboard[i, j] = "0" + j.ToString();
                        }
                        else
                        {
                            gameboard[i, j] =  j.ToString();
                        }
                    }
                }
                if (i < 10)
                {
                    gameboard[i, 0] = "0" + i.ToString();
                }
                else
                {
                    gameboard[i, 0] = i.ToString();
                }
            }
        }
        public void Readout(string [,] gameboard)
        {
            for (int i = 0; i < 21; i++)
            {
                string gameboardline="";
                for (int j = 0; j<21; j++)
                {

                    string charachter = gameboard[i, j];
                   if (i > 0)
                    {
                        gameboardline += charachter + " ";
                    }
                    else 
                    {
                        gameboardline += charachter + "";
                    }                    
                }
                //Console.WriteLine();
                Console.WriteLine(gameboardline);
            }
        }
        public void PlaceShip(int[] start, int lenght, string direction)
        {
            if (direction == "down")
            {
                for (int i = start[0]; i < start[0]+lenght; i++)
                {
                    playerGameBoard[i, start[1]] = ship;
                    int[] coordinate = new int[2] { i, start[1] };
                    chooseship(coordinate,lenght);
                }
            }
            else if(direction == "up")
            {
                for (int i = start[0]; i > start[0] -lenght; i--)
                {
                    playerGameBoard[i, start[1]] = ship;
                    int[] coordinate = new int[2] { i, start[1] };
                    chooseship(coordinate, lenght);
                }
            }
            else if (direction == "left")
            {
                for (int i = start[1]; i >start[1] - lenght; i--)
                {
                    playerGameBoard[start[0], i] = ship;
                    int[] coordinate = new int[2] { start[0], i };
                    chooseship(coordinate, lenght);
                }
            }
            else if (direction == "right")
            {
                for (int i = start[1]; i < start[1] + lenght; i++)
                {
                    playerGameBoard[start[0], i] = ship;
                    int[] coordinate = new int[2] { start[0], i };
                    chooseship(coordinate, lenght);
                }
            }
        }
        public bool Edgecheck(int[] start, int length, string direction)
        {

            bool output = false;
            
                switch (direction)
                {
                    case "up":
                        if (start[0] - length > 0)
                        {                            
                            output = PlaceShipCheck(start, length, direction);
                         }

                        break;
                    case "down":
                        if (start[0] + length< 21)
                        {
                         output = PlaceShipCheck(start, length, direction);
                        }
                         break;
                    case "left":
                        if (start[1] - length > 0)
                        {
                            output = PlaceShipCheck(start, length, direction);
                         }
                    break;
                    case "right":
                        if (start[1] + length< 21)
                        {
                            
                            output = PlaceShipCheck(start, length, direction);
                        }
                        break;
                }
            
            return output;
        }
        public bool TestShip(int [] start,int length, string direction)
        {
            
            bool output = false;
            output = Edgecheck(start, length, direction);
            if (output)
            {
                PlaceShip(start, length, direction);
            }
          return output;
        }
    }
}
