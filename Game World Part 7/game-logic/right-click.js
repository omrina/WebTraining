import { getLocation } from './board-info.js';
import { updateFlagsDisplay } from '../game-bar/game-bar.js';

export const onRightClick = (tile, board) => {
  const [row, col] = getLocation(tile);
  const tileData = board[row][col];

  tile.classList.remove(tileData.currentMarkState());
  tileData.markingStates.push(tileData.markingStates.shift());
  tile.classList.add(tileData.currentMarkState());
  updateFlagsDisplay(board);
};