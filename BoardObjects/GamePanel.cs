
using System.ComponentModel;

namespace BattleShipGame.BoardObjects
{
    public class GamePanel
    {
        public AttackerShipType AttackerShipType { get; set; }
        public BoardCorrdinates BoardCorrdinates { get; set; }

        public GamePanel(int row, int column)
        {
            BoardCorrdinates = new BoardCorrdinates(row, column);
            AttackerShipType = AttackerShipType.Empty;
        }

        public string Status
        {
            get
            {
                return AttackerShipType.GetAttributeOfType<DescriptionAttribute>().Description;
            }
        }

        public bool IsOccupied
        {
            get
            {
                return AttackerShipType == AttackerShipType.Battleship
                                                           || AttackerShipType == AttackerShipType.Cruiser
                                                           || AttackerShipType == AttackerShipType.Destroyer
                                                           || AttackerShipType == AttackerShipType.Carrier
                                                           || AttackerShipType == AttackerShipType.Submarine;
            }
        }

        public bool IsRandomAvailable
        {
            get
            {
                return (BoardCorrdinates.Row % 2 == 0 && BoardCorrdinates.Column % 2 == 0)
                    || (BoardCorrdinates.Row % 2 == 1 && BoardCorrdinates.Column % 2 == 1);
            }
        }
    }

}
