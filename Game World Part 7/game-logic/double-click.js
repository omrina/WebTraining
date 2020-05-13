import { getNeighborsData, getLocation, getNeighborsElements } from './board-info.js';
import { revealTile } from './tile-reveal.js';
import { onMineReveal } from './mine-reveal.js';

export const onDoubleClick = (tile, board) => {
  const neighborsData = getNeighborsData(getLocation(tile), board);

  if (isNeighborFlagsAsMineCount(neighborsData)) {
    neighborsData.some(x => x.isMine && !x.isMarked())
      ? onMineReveal(tile, board)
      : getNeighborsElements(neighborsData, board).forEach(x => revealTile(x, board));
  }
};

const isNeighborFlagsAsMineCount = neighborsData => {
  const minesCount = neighborsData.filter(x => x.isMine).length;

  return neighborsData.filter(x => x.isFlagged()).length === minesCount;
};