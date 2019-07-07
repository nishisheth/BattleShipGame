using System;
namespace BattleShipGame.GameObjects
{
    public class Cruiser :Ship
    {
        public Cruiser()
        {
            Name = "Cruiser";
            Width = 3;
            AttackerShipType = AttackerShipType.Cruiser;
        }
    }
}
