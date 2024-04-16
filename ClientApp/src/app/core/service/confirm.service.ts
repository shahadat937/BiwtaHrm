import { Injectable } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Observable } from 'rxjs';
import { ConfirmDialogComponent } from '../../modals/confirm-dialog/confirm-dialog.component';


@Injectable({
  providedIn: 'root'
})
export class ConfirmService {
  bsModelRef!: BsModalRef;

  constructor(private modalService: BsModalService) { }

  confirm(title = 'Confirmation', 
    message = 'Are you sure you want to do this?', 
    btnOkText = 'Ok', 
    btnCancelText = 'Cancel'): Observable<boolean> {
      const config = {
        initialState: {
          title, 
          message,
          btnOkText,
          btnCancelText
        }
      }
    this.bsModelRef = this.modalService.show(ConfirmDialogComponent, config);
    
    return new Observable<boolean>(this.getResult());
  }

  private getResult() {
    return (observer:any) => {
      // Check if this.bsModelRef is defined and if onHidden is available
      if (this.bsModelRef && this.bsModelRef.onHidden) {
        const subscription = this.bsModelRef.onHidden.subscribe(() => {
          if (this.bsModelRef && this.bsModelRef.content) {
            observer.next(this.bsModelRef.content.result);
            observer.complete();
          }
        });
  
        return {
          unsubscribe() {
            subscription.unsubscribe();
          }
        };
      } else {
        // Handle the case when this.bsModelRef.onHidden is undefined
        // For example, you can complete the observer immediately
        observer.error("bsModelRef.onHidden is undefined");
        return {
          unsubscribe() {
            // No need to unsubscribe as there's no subscription
          }
        };
      }
    };
  }
}