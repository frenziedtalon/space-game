"use strict";
class MouseHelper {
	isDrag(startPos: Array<number>, endPos: Array<number>): boolean {
		return !this.isClick(startPos, endPos);
	}

	isClick(startPos: Array<number>, endPos: Array<number>): boolean {
		return (endPos[0] === startPos[0] && endPos[1] === startPos[1]);
	}
}