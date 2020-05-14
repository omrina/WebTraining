import { markingClasses, flaggedClass, unmarkedClass } from '../game-settings/game-settings.js';
import { updateFlagsDisplay, displayGameOverMessage } from './game.js';

export class Tile {
    constructor (board, element) {
        this.board = board;
        this.element = element;
        this.isMine = false;
        this.isRevealed = false;
        this.currentMarking = markingClasses[0];
        this.bindClickEvents();
    }
    isFlagged = () => this.currentMarking === flaggedClass;
    isMarked = () => this.currentMarking !== unmarkedClass;
    bindClickEvents = () => {
        this.element.addEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('click', this.onOneClick);
    }
    onRightClick = event => {
        event.preventDefault();
        const currentMarkingIndex = markingClasses.indexOf(this.currentMarking);
        const previousMarking = this.currentMarking;
        const nextMarking = markingClasses[(currentMarkingIndex + 1) % markingClasses.length];

        this.currentMarking = nextMarking;

        if (previousMarking === flaggedClass || nextMarking === flaggedClass) {
            updateFlagsDisplay(this.board.data);
        }

        this.element.classList.remove(previousMarking);
        this.element.classList.add(nextMarking);
    }
    onOneClick = () => {
        if (!this.isMarked()) {
            this.isMine ? this.loseGameScenario() : this.revealTile();
        }
    }
    loseGameScenario = () => {
        this.board.revealAllMines();
        displayGameOverMessage(this.board.element, 'lose-cover');
    }
    revealTile = () => {
        const neighborTiles = this.board.getNeighborTiles(this);
        const minesCount = neighborTiles.filter(({ isMine }) => isMine).length;

        this.revealNumber(minesCount);
      
        if (!minesCount) {
            neighborTiles.filter(({ isRevealed }) => !isRevealed)
                .forEach(tile => { tile.revealTile() });
        }
    }
    revealNumber = minesCount => {
        const numbersClasses = ['zero', 'number-one', 'number-two', 'number-three', 'number-four',
            'number-five', 'number-six', 'number-seven', 'number-eight'];

        this.isRevealed = true;
        this.element.className = `tile ${numbersClasses[minesCount]}`;
        this.element.textContent = minesCount ? minesCount : '';
        this.rebindClicksAsRevealedTile();
        
        if (this.board.isGameWon()) {
            displayGameOverMessage(this.board.element, 'win-cover');
        }
    }
    rebindClicksAsRevealedTile = () => {
        this.element.removeEventListener('click', this.onOneClick);
        this.element.removeEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('dblclick', this.onDoubleClick);
    }
    onDoubleClick = () => {
        if (this.board.isNeighborFlagsMatchTileNumber(this)) {
            this.board.getNeighborTiles(this).forEach(tile => { tile.onOneClick() });
        }
    }
}