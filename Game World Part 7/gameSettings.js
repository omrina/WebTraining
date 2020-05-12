const easyMode = { rowsCount: 9, columnsCount: 9, minesCount: 10 };
const mediumMode = { rowsCount: 16, columnsCount: 16, minesCount: 40 };
const hardMode = { rowsCount: 16, columnsCount: 30, minesCount: 99 };
const availableMarkingClasses = ['unmarked', 'flagged-tile', 'questioned-tile'];
const [unmarkedClass, flaggedClass, questionMarkingClass] = [...availableMarkingClasses];
export const settings = {...easyMode, availableMarkingClasses, flaggedClass, unmarkedClass};