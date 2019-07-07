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
        public GameBoard FiredShotBoard { get; set; }

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
            FiredShotBoard = new GameBoard();
        }

        
        /// <summary>
        /// prints User battleship board and firing board 
        /// </summary>
        public void PrintBoards()
        {
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
                    Console.Write(FiredShotBoard.Panels.At(row, firingColumn).Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }


        /// <summary>
        /// Places the ships randomly on 10x10 board 
        /// </summary>
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


        /// <summary>
        /// Takes row and cloumn input from user and fires a shot.
        /// </summary>
        /// <returns>The coordinates of fired shot</returns>
        public BoardCorrdinates FireShot()
        {
            BoardCorrdinates coordinates = null;

            try
			{
				//Take user input to fire a shot
                Console.WriteLine("\nPlease enter coordinates to fire a shot: ");
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
				
			}
			catch(System.FormatException)
			{
				Console.WriteLine("---->>>>>>>> Please enter valid coordinates.<<<<<<<<----");
			}

          
            return coordinates;
        }


        /// <summary>
        /// Chceks if user input is valid or not
        /// </summary>
        /// <returns><c>true</c>, if user input was checked, <c>false</c> otherwise.</returns>
        /// <param name="row">Row.</param>
        /// <param name="column">Column.</param>
        private bool CheckUserInput(int row, int column)
        {
            if ((row > 10 || column > 10) || (row < 1 || column < 1))
                return false;

            return true;
        }

        /// <summary>
        /// Processes the fired shot
        /// </summary>
        /// <returns>Shot is missed or hit</returns>
        /// <param name="coords">Coordinates</param>
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
                Console.WriteLine(" > \"You have sunk " + ship.Name + ".\"\n\n");
            }
            return ShotResult.Hit;
        }


        /// <summary>
        /// Updated a shot on fired board. 
        /// </summary>
        /// <param name="coords">Coords.</param>
        /// <param name="result">Result.</param>
        public void ProcessShotResult(BoardCorrdinates coords, ShotResult result)
        {
            var panel = FiredShotBoard.Panels.At(coords.Row, coords.Column);
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
