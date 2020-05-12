export const buildBoardRowsElements = (rowsCount, colsCount) => {
    const rows = Array(rowsCount).fill().map(() => buildRow());

    return rows.map(row => {
        const tiles = Array(colsCount).fill().map(() => buildTile());
        tiles.forEach(tile => row.appendChild(tile));

        return row;
    });
}

const buildRow = () => {
    const row = document.createElement('div');
    row.classList.add('row');

    return row; 
}

const buildTile = () => {
    const tile = document.createElement('div');
    tile.classList.add('tile');
    tile.classList.add('unrevealed-tile');

    return tile;
}