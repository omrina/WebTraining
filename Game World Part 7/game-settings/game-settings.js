import { onRestart } from '../game-logic/game.js';

let markingClasses = ['unmarked', 'flagged-tile', 'questioned-tile'];
const [unmarkedClass, flaggedClass, questionMarkingClass] = [...markingClasses];
const [easyMode, mediumMode, hardMode] = [{ rowsCount: 9, columnsCount: 9, minesCount: 10 },
                                          { rowsCount: 16, columnsCount: 16, minesCount: 40 },
                                          { rowsCount: 16, columnsCount: 30, minesCount: 99 }];
export { markingClasses, flaggedClass, unmarkedClass };
export let selectedModeSettings = easyMode;

const bindSidebarToggeling = () => {
  document.getElementsByClassName('settings-button')[0].addEventListener('click', toggleSidebar);
  document.getElementsByClassName('close-settings-button')[0].addEventListener('click', toggleSidebar);
};

const toggleSidebar = () => {
  const sidebar = document.getElementsByClassName('settings-sidebar')[0];
  const gameContainer = document.getElementsByClassName('game-container')[0];

  gameContainer.classList.toggle('disabled');
  sidebar.classList.toggle('opened');
};

const bindModeButtons = () => {
  const modeButtons = [...document.getElementsByClassName('mode-button')];
  const [easyButton, mediumButton, hardButton] = 
    [modeButtons.find(x => x.classList.contains('easy-button')),
    modeButtons.find(x => x.classList.contains('medium-button')),
    modeButtons.find(x => x.classList.contains('hard-button'))]

  easyButton.addEventListener('click', () => changeMode(easyButton, easyMode, modeButtons));
  mediumButton.addEventListener('click', () => changeMode(mediumButton, mediumMode, modeButtons));
  hardButton.addEventListener('click', () => changeMode(hardButton, hardMode, modeButtons));
};

const changeMode = (modeButton, modeSettings, modesButtons) => {
  const activeModeClass = 'selected-mode';

  if (!modeButton.classList.contains(activeModeClass)) {
    modesButtons.forEach(x => { x.classList.remove(activeModeClass) });
    modeButton.classList.add(activeModeClass);
    selectedModeSettings = modeSettings;
    onRestart();
  }
};

const bindQuestionMarkSwitch = () => {
  document.getElementsByClassName('question-mark-checkbox')[0]
    .addEventListener('change', toggleQuestionMarking);
};

const toggleQuestionMarking = ({target: checkboxElement}) => {
  if (checkboxElement.checked) {
    markingClasses = [...markingClasses, questionMarkingClass];
  } else {
    markingClasses.pop();
  }

  onRestart();
};

bindSidebarToggeling();
bindModeButtons();
bindQuestionMarkSwitch();