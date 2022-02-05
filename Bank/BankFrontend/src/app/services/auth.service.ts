import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService{
  private subject = new Subject<any>();

  constructor() { }

  sendTokenChangeEvent(){
    this.subject.next(null);
  }

  getTokenChangeEvent(): Observable<any> {
    return this.subject.asObservable();
  }
}
