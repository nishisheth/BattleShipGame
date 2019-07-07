using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame.BoardObjects
{
    public static class PanelExtension
    {
        public static GamePanel At(this List<GamePanel> panels, int row, int column)
        {
            return panels.Where(x => x.BoardCorrdinates.Row == row && x.BoardCorrdinates.Column == column).First();
        }

        public static List<GamePanel> Range(this List<GamePanel> panels, int startRow, int startColumn, int endRow, int endColumn)
        {
            return panels.Where(x => x.BoardCorrdinates.Row >= startRow
                                && x.BoardCorrdinates.Column >= startColumn
                                && x.BoardCorrdinates.Row <= endRow
                                && x.BoardCorrdinates.Column <= endColumn).ToList();
        }
    }
}
