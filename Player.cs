using System;
using System.Collections.Generic;
using System.Linq;
using BattleShipGame.BoardObjects;
using BattleShipGame.GameObjects;

namespace BattleShipGame
{
    public class Player
    {
        public string Name { get; set; }
        public GameBoard GameBoard { get; set; }
        public FiringShotOnBoard FiringShotOnBoard { get; set; }
        public List<Ship> ShipTypes { get; set; }

        //variable returns if all ships are sunk or not for a player
        public bool HasLost
        {
            get
            {
                return ShipTypes.All(x => x.IsSunk);
            }
        }

        public Player(string name)
        {
            Name = name;
            ShipTypes = new List<Ship>()
            {
                new Destroyer(),
                new Submarine(),
                new Cruiser(),
                new Carrier(),
                new Battleship()
            };
           
            GameBoard = new GameBoard();
            FiringShotOnBoard = new FiringShotOnBoard();
        }

        
        /// <summary>
        /// prints User battleship board and firing board 
        /// </summary>
        public void PrintBoards()
        {
            Console.WriteLine(Name);
            Console.WriteLine("Own Battleship Board:                Fired Shot Board:");
            for (int row = 1; row <= 10; row++)
            {
                for (int ownColumn = 1; ownColumn <= 10; ownColumn++)
                {
                    Console.Write(GameBoard.Panels.At(row, ownColumn).Status + " ");
                }
                Console.Write("                ");
                for (int firingColumn = 1; firingColumn <= 10; firingColumn++)
                {
                    Console.Write(FiringShotOnBoard.Panels.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }

        public void PlaceShips()
        {
         
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in ShipTypes)
            {
                bool isOpen = true;
                while (isOpen)
                {
                    var startcolumn = rand.Next(1, 11);
                    var startrow = rand.Next(1, 11);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                    List<int> panelNumbers = new List<int>();
                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Width; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //We cannot place ships beyond the boundaries of the board
                    if (endrow > 10 || endcolumn > 10)
                    {
                        isOpen = true;
                        continue;
                    }

                    //Check if specified panels are occupied
                    var affectedPanels = GameBoard.Panels.Range(startrow, startcolumn, endrow, endcolumn);
                    if (affectedPanels.Any(x => x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach (var panel in affectedPanels)
                    {
                        panel.AttackerShipType = ship.AttackerShipType;
                    }
                    isOpen = false;
                }
            }
        }

        public BoardCorrdinates FireShot()
        {
            BoardCorrdinates coordinates = null;

            //Take user input to fire a shot
            Console.WriteLine("Please enter coordinates to fire a shot: ");
            Console.Write("row: ");
            var row = int.Parse(Console.ReadLine());
            Console.Write("column: ");
            var column = int.Parse(Console.ReadLine());

            if(CheckUserInput(row,column))
            {
                //create coordiantes object to fire a shot 
                coordinates = new BoardCorrdinates(row, column);
                Console.WriteLine(" > \"Firing shot at " + row + ", " + column + "\"");
            }
            else
                Console.WriteLine("---->>>>>>>> Please enter valid coordinates.<<<<<<<<----");
            
            return coordinates;
        }


        private bool CheckUserInput(int row, int column)
        {
            if ((row > 10 || column > 10) || (row < 1 || column < 1))
                return false;

            return true;
        }

        public ShotResult ProcessShot(BoardCorrdinates coords)
        {
            var panel = GameBoard.Panels.At(coords.Row, coords.Column);
            if (!panel.IsOccupied)
            {
                Console.WriteLine(" > \"You missed shot!\"");
                return ShotResult.Miss;
            }
            var ship = ShipTypes.First(x => x.AttackerShipType == panel.AttackerShipType);
            ship.Hits++;
            Console.WriteLine(" > \"It's a Hit!\"");
            if (ship.IsSunk)
            {
                Console.WriteLine(" > \"You have sunk " + ship.Name + ".\"");
            }
            return ShotResult.Hit;
        }

        public void ProcessShotResult(BoardCorrdinates coords, ShotResult result)
        {
            var panel = FiringShotOnBoard.Panels.At(coords.Row, coords.Column);
            switch (result)
            {
                case ShotResult.Hit:
                    panel.AttackerShipType = AttackerShipType.Hit;
                    break;

                default:
                    panel.AttackerShipType = AttackerShipType.Miss;
                    break;
            }
        }

    }
}
