import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit{
  items: MenuItem[] | undefined;
  activeItem: MenuItem | undefined;
ngOnInit(): void {
    this.items = [
    {label: "Manage", routerLink: ['/manage'],icon:'pi pi-fw pi-home'},
    {label: "Presents", icon: "pi pi-fw pi-gift" , routerLink: ['/presents']},
    {label: "Donars", routerLink: ['/donars'],icon:'pi pi-fw pi-user'},
    {label: "Users", routerLink: ['/users'],icon:'pi pi-fw pi-user'},

      ];
  this.activeItem = this.items[0];


}

onActiveItemChange(event: MenuItem) {
  this.activeItem = event;
}
}
