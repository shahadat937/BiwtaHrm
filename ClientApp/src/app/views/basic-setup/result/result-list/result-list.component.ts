import { Component, OnInit } from '@angular/core';
import { MasterData } from 'src/assets/data/master-data';
import { Result } from 'src/app/views/basic-setup/model/result';

@Component({
  selector: 'app-result-list',
  standalone: true,
  imports: [],
  templateUrl: './result-list.component.html',
  styleUrl: './result-list.component.scss'
})
export class ResultListComponent implements OnInit{

  masterData = MasterData;
  loading = false;
  ELEMENT_DATA: Result[] = [];
  isLoading = false;

  
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
  // getResult() {
  //   this.isLoading = true;
  //   this.Result.getGenders(this.paging.pageIndex, this.paging.pageSize,this.searchText).subscribe(response => {
  //     this.dataSource.data = response.items; 
  //     this.paging.length = response.totalItemsCount    
  //     this.isLoading = false;
  //   })
  // }
}




