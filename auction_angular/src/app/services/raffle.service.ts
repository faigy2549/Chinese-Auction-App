import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map } from "rxjs";
import { Present } from '../models/Present.model';
import { HttpClient } from '@angular/common/http';
import { Raffle } from '../models/Raffle.model';

@Injectable({
  providedIn: 'root'
})
export class RaffleService {

  constructor(private httpClient: HttpClient) { }
  private reloadRaffleSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadRaffles$: Observable<boolean> = this.reloadRaffleSubject.asObservable();

  Raffle(p:Present): Observable<any>{
    let url = 'https://localhost:44342/api/raffle/raffle';
    return this.httpClient.post<any>(url,p);
}
  Income() : Observable<Present>{
    let url = 'https://localhost:44342/api/raffle/Income';
    return this.httpClient.get<Present>(url);
  }
  Results() : Observable<string>{
    let url = 'https://localhost:44342/api/raffle/Results';
    return this.httpClient.get<string>(url);
  }

  savePresent(present: Present) :Observable<boolean>{
    let url = 'https://localhost:44342/api/present/updatePresent';
    return this.httpClient.put<boolean>(url, present);
  }

  setReloadRaffle(){
    let flag = this.reloadRaffleSubject.value;
    this.reloadRaffleSubject.next(!flag);
  }

}
