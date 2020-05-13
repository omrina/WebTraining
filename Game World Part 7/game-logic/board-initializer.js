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

    if (!(areLocationsTheSame(newLocation, firstClickedLocation)) &&
        !(locations.some(x => areLocationsTheSame(x, newLocation)))) {
      locations.push(newLocation);
    }
  }

  return locations;
};

const areLocationsTheSame = (firstLocation, secondLocation) =>
  firstLocation[0] === secondLocation[0] && firstLocation[1] === secondLocation[1];