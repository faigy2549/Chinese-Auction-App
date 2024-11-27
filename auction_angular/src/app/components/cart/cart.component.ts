import { Component, OnInit } from '@angular/core';
import { Present } from 'src/app/models/Present.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderItemService } from 'src/app/services/orderItem.service';
import { OrderItem } from 'src/app/models/OrderItem.model';
import { switchMap, tap } from 'rxjs/operators';
import { PresentService } from 'src/app/services/present.service';
import { forkJoin } from 'rxjs';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  constructor(private orderItemService: OrderItemService,private presentService: PresentService,private router: Router, private activatedRoute: ActivatedRoute){}
  total:number=0;
  cart:OrderItem[]=[];
  userName:string='';
  ngOnInit(): void {
    this.activatedRoute.params.subscribe(d => this.userName = d['userName']);
    this.orderItemService.reloadOrderItems$.pipe(
      switchMap(() => this.orderItemService.getUsersCart()),
      switchMap(oi => {
        this.cart = oi;
        const presents = this.cart.map(element =>
          this.presentService.getPresentById(element.presentId).pipe(
            tap(data => {
              element.present = data;
            })
          )
        );
        return forkJoin(presents);
      })
      ).subscribe(); 
}

payNow(){
  this.cart.forEach(element => {
    this.total+=(element.present.price*element.quantity)
  });
  this.router.navigate(['./payment/' + this.total, {relativeTo: this.activatedRoute}]);
}
continue(){
  this.router.navigateByUrl('raffle/'+this.userName)
}

delete(OI:OrderItem){
  this.orderItemService.reloadOrderItems$.subscribe(x => {
    this.orderItemService.deleteFromCart(OI).subscribe(data => alert("deleted from cart")
    )});
}



}