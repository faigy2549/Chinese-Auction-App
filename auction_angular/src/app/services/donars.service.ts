import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Donar } from '../models/Donar.model';

@Injectable({
  providedIn: 'root'
})
export class DonarsService {
  constructor(private httpClient: HttpClient) { }
  private reloadDonarsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadDonars$: Observable<boolean> = this.reloadDonarsSubject.asObservable();

  getDonars(): Observable<Donar[]>{
    let url = 'https://localhost:44342/api/Donar/GetDonars';
    return this.httpClient.get<Donar[]>(url);
  }

  getDonarById(id: number) : Observable<Donar>{
    let url = 'https://localhost:44342/api/Donar/donar/' + id;
    return this.httpClient.get<Donar>(url);
  }
  getDonarByEmail(email:string) : Observable<Donar>{
    let url = 'https://localhost:44342/api/Donar/Get/donar/email' + email;
    return this.httpClient.get<Donar>(url);
  }
  getDonarByName(name: string) : Observable<Donar>{
    let url = 'https://localhost:44342/api/Donar/Get/donar/name' + name;
    return this.httpClient.get<Donar>(url);
  }

  saveDonar(donar: Donar) :Observable<boolean>{
    let url = 'https://localhost:44342/api/Donar/updateDonar';
    return this.httpClient.put<boolean>(url, donar); 
  }

  addDonar(donar: Donar) :Observable<number> {
    let url = 'https://localhost:44342/api/Donar/addDonar';
    return this.httpClient.post<number>(url, donar)
  }
  deleteDonar(number: number) :Observable<boolean> {
    let url = 'https://localhost:44342/api/Donar/deleteDonar/'+number;
    return this.httpClient.delete<boolean>(url)
  }

  setReloadDonar(){
    let flag = this.reloadDonarsSubject.value;
    this.reloadDonarsSubject.next(!flag);
  }
}
