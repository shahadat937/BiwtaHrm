import { Component, NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ManageDeviceComponent } from "./manage-device/manage-device.component";
import { AddDeviceComponent } from "./add-device/add-device.component";

const router = [
    {
        path: "manageDevice",
        component: ManageDeviceComponent,
        data: {
            title: 'Manage Device'
        }
    },
    {
        path:"addDevice",
        component: AddDeviceComponent
    }
]

@NgModule({
  imports: [RouterModule.forChild(router)],
  exports: [RouterModule]
})
export class DeviceRoutingModule {}

