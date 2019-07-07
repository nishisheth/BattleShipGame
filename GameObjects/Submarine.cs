using System;
namespace BattleShipGame.GameObjects
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            Name = "Submarine";
            Width = 3;
            AttackerShipType = AttackerShipType.Submarine;
        }
    }
}
