using System;
namespace BattleShipGame.GameObjects
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Name = "Battleship";
            Width = 4;
            AttackerShipType = AttackerShipType.Battleship;
        }
    }
}
