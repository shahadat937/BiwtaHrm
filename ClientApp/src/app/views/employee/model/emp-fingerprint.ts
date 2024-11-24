export class EmpFingerprint {
    id : number = 0;
	empId : number | null = null;
	rightThumbFile : File | null = null;
	rightIndexFile : File | null = null;
	rightMiddleFile : File | null = null;
	rightRingFile : File | null = null;
	rightLittleFile : File | null = null;
	leftThumbFile : File | null = null;
	leftIndexFile : File | null = null;
	leftMiddleFile : File | null = null;
	leftRingFile : File | null = null;
	leftLittleFile : File | null = null;
	rightThumb : string = '';
	rightIndex : string = '';
	rightMiddle : string = '';
	rightRing : string = '';
	rightLittle : string = '';
	leftThumb : string = '';
	leftIndex : string = '';
	leftMiddle : string = '';
	leftRing : string = '';
	leftLittle : string = '';
    isActive : boolean = true;
}
