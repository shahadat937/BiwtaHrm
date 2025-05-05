import { ShiftSetting } from "./shift-setting";

export class TreeShift {
    id: number = 0;
    shiftName: string | null = null;
    isDefault: boolean = true;
    isActive: boolean = true;
    remark: string | null = null;
    menuPosition: number | null = null;
    shiftSettingDto: ShiftSetting[] = [];
}
