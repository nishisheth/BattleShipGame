using System;
namespace BattleShipGame.GameObjects
{
    public class Destroyer : Ship
    {
        public Destroyer()
        {
            Name = "Destroyer";
            Width = 2;
            AttackerShipType = AttackerShipType.Destroyer;
        }
    }
}
