import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InputFieldSyncService {
  private valueChangeSubject = new Subject<{id: string, value: any}>();

  valueChange$ = this.valueChangeSubject.asObservable();
  constructor() { }

  emitValueChange(id: string, value: string) {
    this.valueChangeSubject.next({id: id, value: value});
  }
}
