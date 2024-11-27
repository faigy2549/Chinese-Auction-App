import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Order } from '../models/Order.model';
import { OrderItem } from '../models/OrderItem.model';
import { OIDTO } from '../models/OIDTO.model';

@Injectable({
  providedIn: 'root'
})
export class OrderItemService {

  constructor(private httpClient: HttpClient) { }
  private reloadOrderItemsSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadOrderItems$: Observable<boolean> = this.reloadOrderItemsSubject.asObservable();

  addToCart(orderitem:OIDTO) :Observable<OrderItem> {
    let url = 'https://localhost:44342/api/OrderItem/createOrderItem';
    return this.httpClient.post<OrderItem>(url, orderitem)
  }
  getUsersCart(): Observable<OrderItem[]>{
    let url = 'https://localhost:44342/api/OrderItem/GetCart';
    return this.httpClient.get<OrderItem[]>(url);
  }
  updateCart(orderitem:OrderItem): Observable<OrderItem[]>{
    let url = 'https://localhost:44342/api/OrderItem/updateItem';
    return this.httpClient.put<OrderItem[]>(url,orderitem);
  }
 deleteFromCart(oi:OrderItem): Observable<Order[]>{
    let url = 'https://localhost:44342/api/OrderItem/deleteItem/'+oi.id;
    return this.httpClient.delete<Order[]>(url);
  }
  setReloadOrderItems(){
    let flag = this.reloadOrderItemsSubject.value;
    this.reloadOrderItemsSubject.next(!flag);
  }

}