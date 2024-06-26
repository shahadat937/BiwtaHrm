export class EmpPhotoSignModule { 
  id: number = 0;
  empId: any=null;
  pNo: string = "";
  photoUrl: string = "";
  signatureUrl: string = "";
  photoFile: File | null = null;
  signatureFile: File | null = null;
  uniqueIdentity: string = "";
  remark: string = "";
  menuPosition: number = 0;
  isActive: boolean = true
}
