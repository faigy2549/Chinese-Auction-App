import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Order } from 'src/app/models/Order.model';
import { OrderItem } from 'src/app/models/OrderItem.model';
import { Present } from 'src/app/models/Present.model';
import { User } from 'src/app/models/User.model';
import { OrderService } from 'src/app/services/order.service';
import { OrderItemService } from 'src/app/services/orderItem.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {

  constructor(public orderItemService:OrderItemService,public orderService:OrderService,public userServcie: UsersService,private messageService: MessageService, private confirmationService: ConfirmationService,private router: Router, private activatedRoute: ActivatedRoute){}
  total:number=0;
  order:Order=new Order;
  userName:string='';
user:User=new User;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(d => this.total = d['total'])

  }
  
  createOrder(){
    this.orderItemService.getUsersCart().subscribe(data => {this.order.orderItems= data
      this.order.sum=this.total;
      this.orderService.createOrder(this.order).subscribe(a => {
        this.orderService.setReloadOrder();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Order Created', life: 3000 });
        alert("thanks for your order #"+a.orderId+"🎉");
        sessionStorage.removeItem("MyCart");
      });
      this.router.navigateByUrl('raffle/'+this.userName);
      this.order=new Order;}
      )
   
  }
  
  }

 


