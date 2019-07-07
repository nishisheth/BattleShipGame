using System.ComponentModel;

namespace BattleShipGame
{
 
        public enum AttackerShipType
        {

            [Description("_")]
            Empty,

            [Description("X")]
            Hit,

            [Description("M")]
            Miss,

            [Description("A")]
            Carrier,

            [Description("C")]
            Cruiser,

            [Description("B")]
            Battleship,

            [Description("S")]
            Submarine,

            [Description("D")]
            Destroyer

           
        }

        public enum ShotResult
        {
            Miss,
            Hit
        }
}
