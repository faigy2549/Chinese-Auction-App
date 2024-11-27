import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from "rxjs";
import { Present } from '../models/Present.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PresentService {

  constructor(private httpClient: HttpClient) { }
  private reloadPresentsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadPresents$: Observable<boolean> = this.reloadPresentsSubject.asObservable();

  getPresents(orderby:string): Observable<Present[]>{
    let url = 'https://localhost:44342/api/present/GetPresents/'+orderby;
    return this.httpClient.get<Present[]>(url);
}

  getPresentById(number: number) : Observable<Present>{
    let url = 'https://localhost:44342/api/Present/present/' + number;
    return this.httpClient.get<Present>(url);
  }

  
  getPresentByFilter(amount?: string,donarname?:string,name?:string) : Observable<Present>{
    let url = 'https://localhost:44342/api/Present/present/' + amount;
    return this.httpClient.get<Present>(url);
  }
  getPresentsByCategory(c:number): Observable<Present[]>{
    let url = 'https://localhost:44342/api/Present/getpresentbycategory/' + c;
    return this.httpClient.get<Present[]>(url);
  }
  savePresent(present: Present) :Observable<boolean>{
    let url = 'https://localhost:44342/api/present/updatePresent';
    return this.httpClient.put<boolean>(url, present);
  }

  addPresent(present: Present) :Observable<number> {
    let url = 'https://localhost:44342/api/present/addPresent';
    return this.httpClient.post<number>(url, present)
  }
  deletePresent(number: number) :Observable<boolean> {
    let url = 'https://localhost:44342/api/Present/deletePresent/'+number;
    return this.httpClient.delete<boolean>(url)
  }

  setReloadPresent(){
    let flag = this.reloadPresentsSubject.value;
    this.reloadPresentsSubject.next(!flag);
  }

}
