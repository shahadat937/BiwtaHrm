export class EmpCountOnReportingDto {
    totalAssigned: number = 0;
    totalNull: number = 0;
    countReportingInfo: CountReportingInfo[] = [];
}
export class CountReportingInfo {
    id: number = 0;
    name: string = '';
    count: number = 0;
}
