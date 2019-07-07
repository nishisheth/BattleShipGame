using System;
using BattleShipGame.BoardObjects;
using System.Net;
namespace BattleShipGame.GameObjects
{
    public class Game
    {
        public Player Player { get; set; }

        public Game(String PlayerName)
        {
            Player = new Player(PlayerName);

            // Please all attacker ships in random coordiantes based on their size
            Player.PlaceShips();
        }

        public void PlayRound()
        {

            //create coordiantes object to fire a shot 
            BoardCorrdinates coordinates = Player.FireShot();


            if(coordinates!=null)
            {
                //process a shot 
                var result = Player.ProcessShot(coordinates);

                //Updates user fired shot coordinates on fire board 
                Player.ProcessShotResult(coordinates, result);
            }
           
        }

        public void PlayToEnd()
        {
            Player.PrintBoards();

            while (!Player.HasLost)
            {
                PlayRound();
            }

            Player.PrintBoards();
            Console.WriteLine("Sorry you have lost. All of your ships are sunk");
        }
    }
}
