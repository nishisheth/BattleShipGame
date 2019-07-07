using System;
using BattleShipGame.GameObjects;

namespace BattleShipGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter Player name: ");
            var playerName = Console.ReadLine();

            Game game = new Game(playerName);
            game.PlayToEnd();

        }
    }
}
