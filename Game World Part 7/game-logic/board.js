import { Tile } from './tile.js'
import { Location } from './location.js'

export class Board {
    constructor (rowsCount, colsCount, minesCount) {
        this.rowsCount = rowsCount;
        this.colsCount = colsCount;
        this.minesCount = minesCount;
        this.flaggedTilesCount = 0;
        this.element = document.getElementById('game-board');
        this.resetBoardElement();
        this.data = this.initializeBoard();
        this.bindFirstTileClick();
    }

    resetBoardElement = () => {
        this.element.classList.remove('disabled');
        this.element.textContent = '';
    }

    initializeBoard = () => {
        const board = [];

        for (let row = 0; row < this.rowsCount; row++) {
            const rowElement = this.createRowElement(this.colsCount);
            this.element.appendChild(rowElement);
            board[row] = [];

            for (let col = 0; col < this.colsCount; col++) {
                board[row][col] = new Tile(rowElement.children[col]);         
            }
        }

        return board;
    }

    createRowElement = tileCount => {
        const rowElement = document.createElement('div');

        rowElement.className = 'row';

        for (let i = 0; i < tileCount; i++) {
            rowElement.appendChild(this.createTileElement())
        }

        return rowElement;
    }

    createTileElement = () => {
        const tileElement = document.createElement('div');

        tileElement.className = 'tile unrevealed-tile';

        return tileElement;
    }

    bindFirstTileClick = () => {
        this.element.addEventListener('click', ({target: tileElement}) => {
            this.onFirstTileClick(tileElement);
        }, { once: true , capture: true});
    }

    onFirstTileClick = tileElement => {
        const tiles = this.data.flat();
        const tilesElements = tiles.map(({ element }) => element);

        if (tilesElements.includes(tileElement)){
            const clickedTile = tiles.find(({ element }) => tileElement === element);
            
            this.assignMinesToBoard(this.getLocation(clickedTile));

            return;
        }
        
        this.bindFirstTileClick();
    }

    getLocation = tile => {
        const rowIndex = this.data.findIndex(row => row.includes(tile));
        const colIndex = this.data[rowIndex].indexOf(tile);
        
        return new Location(rowIndex, colIndex);
    }

    assignMinesToBoard = firstClickedLocation => {
        const minesLocations = this.generateMinesLocations([], firstClickedLocation);

        minesLocations.forEach(({row, col}) => {
            this.data[row][col].isMine = true;
        });
    }

    generateMinesLocations = (locations, firstClickedLocation) => {
        if (locations.length === this.minesCount) {
            return locations;
        }

        const newLocation = new Location (Math.floor(Math.random() * this.rowsCount),
                                            Math.floor(Math.random() * this.colsCount));

        if (!(this.areLocationsTheSame(newLocation, firstClickedLocation)) &&
            !(locations.some(x => this.areLocationsTheSame(x, newLocation)))) {
            locations.push(newLocation);
        }
    
        return this.generateMinesLocations(locations, firstClickedLocation);
    }

    areLocationsTheSame = ({ row: firstRow, col: firstCol }, { row: secondRow, col: secondCol }) => {
      return firstRow === secondRow && firstCol === secondCol;
    }

    getNeighborsInfo = tile => {
        const neighborTiles = this.getNeighborTiles(tile);
        const minedNeighborsCount = this.countMines(neighborTiles);
        const flaggedNeighborsCount = this.countFlagged(neighborTiles);

        return {neighborTiles, minedNeighborsCount, flaggedNeighborsCount};
    }

    getNeighborTiles = tile => {
        const {row, col} = this.getLocation(tile);
        const [rowStart, rowEnd] = [!row ? 0 : row - 1, row + 2];
        const [colStart, colEnd] = [!col ? 0 : col - 1, col + 2];

        return this.data.slice(rowStart, rowEnd)
                         .reduce((neighbors, currentRow) => {
                             return neighbors.concat(currentRow.slice(colStart, colEnd));
                         }, [])
                         .filter(x => x !== tile);
    }

    countMines = tiles => tiles.filter(({ isMine }) => isMine).length;

    countFlagged = tiles => tiles.filter(({ isFlagged }) => isFlagged()).length;

    isGameWon = () => {
        const allTiles = this.data.flat();
        const revealedTilesCount = allTiles.filter(({ isRevealed }) => isRevealed()).length;
        
        return allTiles.length - revealedTilesCount === this.minesCount;
    }

    revealAllMines = () => {
        const mineElements = this.data.flat().filter(({ isMine }) => isMine)
                                              .map(({ element }) => element);

        mineElements.forEach(x => {
            x.classList.remove('unrevealed-tile');
            x.classList.add('mined-tile');
        });
    }
}