import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/User.model';
import { Order } from '../models/Order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClient: HttpClient) { }
  private reloadOrderSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadOrders$: Observable<boolean> = this.reloadOrderSubject.asObservable();

  createOrder(order:Order) :Observable<Order> {
    let url = 'https://localhost:44342/api/Order/postOrder';
    return this.httpClient.post<Order>(url, order)
  }
  getOrders(): Observable<Order[]>{
    let url = 'https://localhost:44342/api/order/GetOrders';
    return this.httpClient.get<Order[]>(url);
  }
  setReloadOrder(){
    let flag = this.reloadOrderSubject.value;
    this.reloadOrderSubject.next(!flag);
  }


}
