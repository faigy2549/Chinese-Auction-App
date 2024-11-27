import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { ConfirmationService, MenuItem, MessageService, SelectItem } from 'primeng/api';
 import { forkJoin} from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { Order } from 'src/app/models/Order.model';
import { OrderItem } from 'src/app/models/OrderItem.model';
import { Present } from 'src/app/models/Present.model';
import { User } from 'src/app/models/User.model';
import { DonarsService } from 'src/app/services/donars.service';
import { OrderService } from 'src/app/services/order.service';
import { OrderItemService } from 'src/app/services/orderItem.service';
import { PresentService } from 'src/app/services/present.service';
import { RaffleService } from 'src/app/services/raffle.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent implements OnInit {
  activatedRoute: any;
  userName: any;
  PresentService: any;
  orderBYs: SelectItem[] = [
    { label: 'Featured', value: 'Featured' },
    { label: 'Most Expensive', value: 'mostExpensive' },
    { label: 'Most Bought', value: 'mostBought' }
  ];
  @Input()
  orderby:string="Featured";
  constructor(public raffleService:RaffleService,public orderItemService:OrderItemService,public donarService: DonarsService,public userService: UsersService,public orderService:OrderService,public presentService :PresentService, private messageService: MessageService, private confirmationService: ConfirmationService) { }
  today: Date = new Date();
  winner!: User ;
  winners:string[]=[];
  random:number[]=[];
  presents!:Present[];
  orders:Order[]=[];  
  orderitems:OrderItem[]=[];  
  items: MenuItem[] | undefined;
  activeItem: MenuItem | undefined;
  selected:number[]=[];
  p!:Present;
  targetDate!: Date; 
  countdown!: string; 
  ngOnInit(): void {
    this.presentService.reloadPresents$.pipe(
      switchMap(() => this.presentService.getPresents(this.orderby)),
      switchMap(presents => {
        this.presents = presents;
        const donarObservables = this.presents.map(element =>
          this.donarService.getDonarById(element.donarId).pipe(
            tap(data => {
              element.donar = data;
            })
          )
        );
        return forkJoin(donarObservables);
      })
      ).subscribe();
  this.orderService.reloadOrders$.subscribe(x => {
  this.orderService.getOrders().subscribe(data => this.orders = data);
});
  this.targetDate = new Date('2024-2-1'); 
  this.startCountdown();
}

startCountdown(): void {
  this.updateCountdown();
  setInterval(() => {
    this.updateCountdown();
  }, 1000);
}

updateCountdown(): void {
  const now = new Date().getTime();
  const distance = this.targetDate.getTime() - now;
  const days = Math.floor(distance / (1000 * 60 * 60 * 24));
  const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
  const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
  const seconds = Math.floor((distance % (1000 * 60)) / 1000);
  this.countdown = `${days}D:${hours}H:${minutes}M:${seconds}S And We Raffle!`;
}

  onOrderbyChange() {
    this.presentService.getPresents(this.orderby).subscribe(
      presents => {
        this.presents = presents;
        const donarObservables = this.presents.map(element =>
          this.donarService.getDonarById(element.donarId).pipe(
            tap(data => {
              element.donar = data;
            })
          )
        );
        forkJoin(donarObservables).subscribe();
      }
    );
  }

  inc(){
    this.raffleService.Income().subscribe();
  }
  
  res(){
    this.raffleService.Results().subscribe();
  }
  raffle(present: Present) {
    present.donar=null;
    this.raffleService.Raffle(present).subscribe(data=>{
    present.winner=data;
    this.presentService.savePresent(present).subscribe(data=>alert(present.winner))
  })
  }
}
