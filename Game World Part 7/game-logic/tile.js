import { tileStates, markingClasses } from '../game-settings/game-settings.js';
import { updateFlagsData, onDoubleClick, revealTile, loseGame } from './game.js';
import { numbersClasses } from './numbersClasses.js';

export class Tile {
    constructor(element) {
        this.element = element;
        this.isMine = false;
        this.currentState = tileStates.UNMARKED;
        this.bindClickEvents();
    }

    isRevealed = () => this.currentState === tileStates.REVEALED;

    isFlagged = () => this.currentState === tileStates.FLAGGED;

    isUnmarked = () => this.currentState === tileStates.UNMARKED;

    bindClickEvents = () => {
        this.element.addEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('click', this.onOneClick);
    }

    onRightClick = event => {
        event.preventDefault();
        this.changeMarking();
    }

    changeMarking = () => {
        const previousState = this.currentState;

        this.currentState = (this.currentState + 1) % Object.keys(tileStates).length;
        this.currentState = this.currentState !== tileStates.REVEALED
                            ? this.currentState
                            : tileStates.UNMARKED;

        this.element.classList.remove(markingClasses[previousState]);
        this.element.classList.add(markingClasses[this.currentState]);
        updateFlagsData(previousState, this.currentState);
    }

    onOneClick = () => {
        if (this.isUnmarked()) {
            this.isMine ? loseGame() : revealTile(this);
        }
    }

    setAsRevealed = minesCount => {
        this.element.className = `tile ${numbersClasses[minesCount]}`;
        this.element.textContent = minesCount ? minesCount : '';
        this.currentState = tileStates.REVEALED;
        this.rebindClicksAsRevealedTile();
    }

    rebindClicksAsRevealedTile = () => {
        this.element.removeEventListener('click', this.onOneClick);
        this.element.removeEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('dblclick', () => onDoubleClick(this));
    }
}