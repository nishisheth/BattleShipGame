using System;
namespace BattleShipGame.GameObjects
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Name = "Aircraft Carrier";
            Width = 5;
            AttackerShipType = AttackerShipType.Carrier;
        }
    }
}
