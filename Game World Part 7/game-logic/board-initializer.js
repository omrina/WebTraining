export const initializeBoard = settings => {
  const [isRevealed, isMine, markingStates] = [false, false, [...settings.availableMarkingClasses]];

  return Array(settings.rowsCount).fill().map(() =>
    Array(settings.columnsCount).fill().map(() => Object.assign({},
      {
        isRevealed,
        isMine,
        markingStates: [...markingStates],
        currentMarkState () { return this.markingStates[0]; },
        isMarked () { return this.currentMarkState() !== settings.unmarkedClass; },
        isFlagged () { return this.currentMarkState() === settings.flaggedClass; }
      })));
};

export const assignMinesOnBoard = (firstClickedLocation, board, settings) =>
  generateMinesLocations(firstClickedLocation, settings).forEach(location => {
    board[location[0]][location[1]].isMine = true;
  });

const generateMinesLocations = (firstClickedLocation, { rowsCount, columnsCount, minesCount }) => {
  const locations = [];

  while (locations.length < minesCount) {
    const newLocation = [Math.floor(Math.random() * rowsCount),
      Math.floor(Math.random() * columnsCount)];

    if (!(newLocation[0] === firstClickedLocation[0] && newLocation[1] === firstClickedLocation[1]) && !locations.some(location => location[0] === newLocation[0] &&
                                        location[1] === newLocation[1])) {
      locations.push(newLocation);
    }
  }

  return locations;
};