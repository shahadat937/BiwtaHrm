import { Component, OnInit } from '@angular/core';
import { cilOptions, cilArrowTop } from '@coreui/icons';
import { getStyle } from '@coreui/utils';
import { WidgetsService } from '../service/widgets.service';

@Component({
  selector: 'app-users-widgets',
  templateUrl: './users-widgets.component.html',
  styleUrl: './users-widgets.component.scss'
})
export class UsersWidgetsComponent implements OnInit {

  constructor(
    public widgetService: WidgetsService,
  ) {}

  icons = { cilOptions, cilArrowTop };

  widgetsData: any[] = [];
  usersWidgets: any[] = [];
  data: any[] = [];
  options: any[] = [];
  labels = [];
  datasets = [];

  optionsDefault = {
    plugins: {
      legend: {
        display: false
      }
    },
    maintainAspectRatio: false,
    scales: {
      x: {
        grid: {
          display: false,
          drawBorder: false
        },
        ticks: {
          display: false
        }
      },
      y: {
        min: 30,
        max: 89,
        display: false,
        grid: {
          display: false
        },
        ticks: {
          display: false
        }
      }
    },
    elements: {
      line: {
        borderWidth: 1,
        tension: 0.4
      },
      point: {
        radius: 4,
        hitRadius: 10,
        hoverRadius: 4
      }
    }
  };

  ngOnInit(): void {
    this.getAllWidgetsInfo();
    this.getAllUserWidgetsInfo();
  }

  
  getAllWidgetsInfo(){
    this.widgetService.getAllWidgetsInfo().subscribe((item) => {
      this.processWidgetData(item);
      this.widgetsData = item;
    })
  }

  getAllUserWidgetsInfo(){
    this.widgetService.getAllUsersWidgetsInfo().subscribe((item) => {
      this.usersWidgets = item;
    })
  }
  processWidgetData(items: any[]) {
    this.data = items.map(item => ({
      labels: item.labels,
      datasets: [{
        label: item.label,
        backgroundColor: 'rgba(255,255,255,.2)',
        borderColor: 'rgba(255,255,255,.55)',
        pointBackgroundColor: getStyle('--cui-light'),
        pointHoverBorderColor: getStyle('--cui-light'),
        data: item.data
      }]
    }));
    this.setOptions();
  }

  setOptions() {
    for (let idx = 0; idx < 4; idx++) {
      const options = JSON.parse(JSON.stringify(this.optionsDefault));
      switch (idx) {
        case 0: {
          this.options.push(options);
          break;
        }
        case 1: {
          options.scales.y.min = -9;
          options.scales.y.max = 39;
          this.options.push(options);
          break;
        }
        case 2: {
          options.scales.x = { display: false };
          options.scales.y = { display: false };
          options.elements.line.borderWidth = 2;
          options.elements.point.radius = 0;
          this.options.push(options);
          break;
        }
        case 3: {
          options.scales.x.grid = { display: false, drawTicks: false };
          options.scales.x.grid = { display: false, drawTicks: false, drawBorder: false };
          options.scales.y.min = undefined;
          options.scales.y.max = undefined;
          options.elements = {};
          this.options.push(options);
          break;
        }
      }
    }
  }

}