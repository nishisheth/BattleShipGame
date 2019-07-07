using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame.BoardObjects
{
    public class FiringShotOnBoard : GameBoard
    {
        public List<BoardCorrdinates> GetOpenRandomPanels()
        {
            return Panels.Where(x => x.AttackerShipType == AttackerShipType.Empty && x.IsRandomAvailable).Select(x => x.BoardCorrdinates).ToList();
        }

        public List<BoardCorrdinates> GetHitNeighbors()
        {
            List<GamePanel> panels = new List<GamePanel>();
            var hits = Panels.Where(x => x.AttackerShipType == AttackerShipType.Hit);
            foreach (var hit in hits)
            {
                panels.AddRange(GetNeighbors(hit.BoardCorrdinates).ToList());
            }
            return panels.Distinct().Where(x => x.AttackerShipType == AttackerShipType.Empty).Select(x => x.BoardCorrdinates).ToList();
        }

        public List<GamePanel> GetNeighbors(BoardCorrdinates boardCordinates)
        {
            int row = boardCordinates.Row;
            int column = boardCordinates.Column;
            List<GamePanel> panels = new List<GamePanel>();
            if (column > 1)
            {
                panels.Add(Panels.At(row, column - 1));
            }
            if (row > 1)
            {
                panels.Add(Panels.At(row - 1, column));
            }
            if (row < 10)
            {
                panels.Add(Panels.At(row + 1, column));
            }
            if (column < 10)
            {
                panels.Add(Panels.At(row, column + 1));
            }
            return panels;
        }
    }
}
