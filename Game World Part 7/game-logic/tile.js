import { markingClasses } from '../game-settings/game-settings.js';
import { updateFlagsData, onDoubleClick, revealTile, loseGame } from './game.js';
import { numbersClasses } from './numbersClasses.js';

export class Tile {
    constructor (element) {
        this.element = element;
        this.isMine = false;
        this.currentMarking = markingClasses.unmarkedClass;
        this.bindClickEvents();
    }

    isRevealed = () => this.currentMarking === null;

    isFlagged = () => this.currentMarking === markingClasses.flaggedClass;

    isUnmarked = () => this.currentMarking === markingClasses.unmarkedClass;

    bindClickEvents = () => {
        this.element.addEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('click', this.onOneClick);
    }

    onRightClick = event => {
        event.preventDefault();
        this.changeMarking();
    }

    changeMarking = () => {
        const markingClassesValues = Object.values(markingClasses); 
        const currentMarkingIndex = markingClassesValues.indexOf(this.currentMarking);
        const previousMarking = this.currentMarking;

        this.currentMarking = markingClassesValues[(currentMarkingIndex + 1) % markingClassesValues.length];
        this.element.classList.remove(previousMarking);
        this.element.classList.add(this.currentMarking);
        updateFlagsData(previousMarking, this.currentMarking);
    }

    onOneClick = () => {
        if (this.isUnmarked()) {
            this.isMine ? loseGame() : revealTile(this);
        }
    }

    setAsRevealed = minesCount => {
        this.element.className = `tile ${numbersClasses[minesCount]}`;
        this.element.textContent = minesCount ? minesCount : '';
        this.currentMarking = null;
        this.rebindClicksAsRevealedTile();
    }

    rebindClicksAsRevealedTile = () => {
        this.element.removeEventListener('click', this.onOneClick);
        this.element.removeEventListener('contextmenu', this.onRightClick);
        this.element.addEventListener('dblclick', () => onDoubleClick(this));
    }
}