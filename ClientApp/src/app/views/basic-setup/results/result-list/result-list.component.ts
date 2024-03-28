import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ResultService } from 'src/app/views/basic-setup/service/result.service';
import { Result } from '../../model/result';
import { MatTableDataSource } from '@angular/material/table';


@Component({
  selector: 'app-result-list',
  templateUrl: './result-list.component.html',
  styleUrls: ['./result-list.component.scss'] 
})

export class ResultListComponent implements OnInit {
  dataSource: MatTableDataSource<Result> = new MatTableDataSource();
  resultList: Result[] = []; 
  displayedColumns: string[] = ['resultId','resultName','isActive'];
  

  constructor(private router: Router, private resultService: ResultService) {}
  ngOnInit() {
    this.getResults(); 
  }
 
  getResults() {
    this.resultService.getAllResult().subscribe(items => {
      this.resultList = items; 
     console.log(this.resultList);
    });
  }
}
