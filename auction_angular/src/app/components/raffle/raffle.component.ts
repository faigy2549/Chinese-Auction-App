import { Component, OnInit, ViewChild } from '@angular/core';
import { Present } from 'src/app/models/Present.model';
import { PresentService } from 'src/app/services/present.service';
import { ActivatedRoute, Router } from '@angular/router';
import { OrderItemService } from 'src/app/services/orderItem.service';
import { OrderItem } from 'src/app/models/OrderItem.model';
import { OIDTO } from 'src/app/models/OIDTO.model';
import { switchMap, tap } from 'rxjs/operators';
import { forkJoin } from 'rxjs';
import { DonarsService } from 'src/app/services/donars.service';
import { MenuItem, SelectItem } from 'primeng/api';
import { DataView } from 'primeng/dataview';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-raffle',
  templateUrl: './raffle.component.html',
  styleUrls: ['./raffle.component.css']
})
export class RaffleComponent implements OnInit {
  orderby:string="Featured";
  carts: OrderItem[] = [];
  presents!: Present[];
  userName: string = '';
  winners: string[] = [];
  winner: string = '';
  present: Present = new Present;
  oi: OIDTO = new OIDTO;
  profileItems: MenuItem[]=[];

  constructor(private presentService: PresentService,public donarService: DonarsService,
  private orderItemService: OrderItemService, private router: Router,
  private activatedRoute: ActivatedRoute,   private messageService: MessageService) { }
  selectedCategory:any=null;
  categories: any[] = [
      { name: 'Woman', key: 'W' },
      { name: 'Men', key: 'M' },
      { name: 'Kids', key: 'K' },
      { name: 'Home', key: 'H' }
  ];

  orderBYs: SelectItem[] = [
    { label: 'Featured', value: 'Featured' },
    { label: 'Most Expensive', value: 'mostExpensive' },
    { label: 'Most Bought', value: 'mostBought' }
  ];
  userTickets: Present[] = [];
  ngOnInit() {
      this.profileItems = [
        { label: 'Logout', icon: 'pi pi-sign-out', command: () => this.logout() }
      ];
    this.activatedRoute.params.subscribe(d => this.userName = d['userName']);
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

  }
  searchQuery!: string;
  @ViewChild('dv') dv!:DataView;
  applyFilter(): void {
    if (this.searchQuery) {
      this.dv.filter(this.searchQuery);
    } else {
      this.dv.filter('');
    }
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
  bycategory(category:string) {
    if (this.selectedCategory === category) {
      this.selectedCategory = null; 
    } else {
      this.selectedCategory = category; 
    }
    let c=0;
    for(let i=0;i<this.categories.length;i++){
      if(this.categories[i].name==category){
        c=i;
        break;
      }
    }
    this.presentService.getPresentsByCategory(c).subscribe(
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
  addToCart(present: Present) {
        this.oi.presentId = present.presentId;
        this.oi.status = false;
        this.orderItemService.addToCart(this.oi).subscribe(data => {
          this.messageService.add({severity:'success', summary:'Success', detail:'The item has been added to your cart.'});
        });
  } 
  cart() {
    this.router.navigateByUrl('cart/'+this.userName);
  }
  logout(){
    this.router.navigate(['./']);
  }
}

