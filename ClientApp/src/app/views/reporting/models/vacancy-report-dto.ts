export class VacancyReportDto {
    totalPost : number = 0;
    totalInService : number = 0;
    totalVacant : number = 0;
    vacancyDetailsDto: VacancyDetailsDto[] = [];
}
export class VacancyDetailsDto {
    departmentName : string = "";
    sectionName : string = "";
    designationName : string = "";
    totalPost : number = 0;
    totalInService : number = 0;
    totalVacantPost : number = 0;

    departmentId : number = 0;
    sectionSequence : number = 0;
    designationPosition : number = 0;
}
