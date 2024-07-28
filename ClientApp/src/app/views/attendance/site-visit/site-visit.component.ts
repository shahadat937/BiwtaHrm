import { Component, OnInit } from '@angular/core';
import {SiteVisitModel} from "../models/site-visit-model";
import { cilList, cilShieldAlt } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { cilX,cilCheck,cilPencil,cilTrash } from '@coreui/icons';



@Component({
  selector: 'app-site-visit',
  templateUrl: './site-visit.component.html',
  styleUrl: './site-visit.component.scss'
})
export class SiteVisitComponent implements OnInit{

  icons = {cilX,cilTrash,cilCheck, cilPencil}
  ngOnInit(): void {

  }
  getBadgeColor(status:string) {
    switch(status) {
      case 'Completed':
        return "success";
      case 'Pending':
        return "warning";
      case 'In Progress':
        return "warning";

    }

    return "primary";
  }

    tableData: SiteVisitModel[] = [
    {
        siteVisitId: 1,
        empId: 101,
        firstName: 'John',
        lastName: 'Doe',
        fromDate: '2024-07-01',
        toDate: '2024-07-02',
        visitPlace: 'Office',
        visitPurpose: 'Meeting',
        status: 'Completed',
        remark: 'No issues encountered'
    },
    {
        siteVisitId: 2,
        empId: 102,
        firstName: 'Jane',
        lastName: 'Smith',
        fromDate: '2024-07-05',
        toDate: '2024-07-07',
        visitPlace: 'Warehouse',
        visitPurpose: 'Inspection',
        status: 'In Progress',
        remark: 'Initial findings were good'
    },
    {
        siteVisitId: 3,
        empId: 103,
        firstName: 'Emily',
        lastName: 'Johnson',
        fromDate: '2024-07-10',
        toDate: '2024-07-11',
        visitPlace: 'Client Site',
        visitPurpose: 'Consultation',
        status: 'Pending',
        remark: 'Awaiting client feedback'
    },
    {
        siteVisitId: 4,
        empId: 104,
        firstName: 'Michael',
        lastName: 'Brown',
        fromDate: '2024-07-15',
        toDate: '2024-07-16',
        visitPlace: 'Factory',
        visitPurpose: 'Audit',
        status: 'Completed',
        remark: 'Audit completed successfully'
    },
    {
        siteVisitId: 5,
        empId: 105,
        firstName: 'Laura',
        lastName: 'Wilson',
        fromDate: '2024-07-20',
        toDate: '2024-07-22',
        visitPlace: 'Lab',
        visitPurpose: 'Testing',
        status: 'Cancelled',
        remark: 'Testing was postponed'
    }
].map(data => {
    const siteVisit = new SiteVisitModel();
    Object.assign(siteVisit, data);
    return siteVisit;
});
  onApprove(siteVisitId:number) {
    console.log(siteVisitId);
  }
}
