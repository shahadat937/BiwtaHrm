export class PaginatorModel {
    pageSize : number = 10;
    pageIndex : number = 1;
    pageSizeOptions : any = [5, 10, 25, 100, 500, 1000, 5000, 10000];
    searchText : string = '';
    length : number = 0;
}
