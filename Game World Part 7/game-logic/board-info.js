export const getLocation = tile => {
  const parentRow = tile.parentNode;

  return [[...parentRow.parentNode.children].indexOf(parentRow),
    [...parentRow.children].indexOf(tile)];
};

export const getNeighborsData = ([row, col], board) =>
  board.slice(row === 0 ? 0 : row - 1, row + 2).reduce((neighbors, currentRow) =>
    [...neighbors, ...currentRow.slice(col === 0 ? 0 : col - 1, col + 2)], [])
    .filter(x => x !== board[row][col]);

export const getNeighborsElements = (neighborsData, board) =>
  neighborsData.reduce((neighborsElements, neighborData) =>
    [...neighborsElements, getTileElement(...getLocationByData(neighborData, board))], []);

export const getLocationByData = (tileData, board) => {
  const rowIndex = board.findIndex(row => row.includes(tileData));
  const colIndex = board[rowIndex].indexOf(tileData);

  return [rowIndex, colIndex];
};

export const getTileElement = (row, col) => {
  const boardElement = document.getElementsByClassName('game-board')[0];

  return boardElement.children[row].children[col];
};