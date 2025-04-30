export class ShiftSetting {
    id: number = 0;
    settingName: string | null = null;
    shiftTypeId: number | null = null;
    startTime: Date | null = null;
    endTime: Date | null = null;
    bufferTime: Date | null = null;
    absentTime: Date | null = null;
    startDate: Date | null = null;
    endDate: Date | null = null;
    isActive: boolean = true;
    remark: string | null = null;
}
