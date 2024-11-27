import { Component, ViewChild } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { User } from 'src/app/models/User.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
    searchValue: string = "";
    @ViewChild('dt') dt!:Table;
  userDialog: boolean = false;

  users: User[] = [];

  user: User = new User();

    selectedUsers!: User[];

    submitted!: boolean;

    constructor(public userService: UsersService, private messageService: MessageService, private confirmationService: ConfirmationService) { }

    ngOnInit() {
        this.userService.reloadUsers$.subscribe(x => {
            this.userService.getUsers().subscribe(data => this.users = data);
        });
    }

    openNew() {
        this.user = new User();
        this.submitted = false;
        this.userDialog = true;
    }
  
    applyFilter() {
        if (this.dt) {
          this.dt.reset(); 
          this.dt.filterGlobal(this.searchValue, 'contains');
        }
      }


    editUser(user: User) {
        this.user = { ...user };
        this.userService.getUserById(this.user.userId).subscribe(user => this.user = user)
        this.userDialog = true;
    }

    deleteUser(user: User) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + user.email + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {          
                this.userService.deleteUser(user.userId).subscribe(c=>{
                this.userService.setReloadUser();
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'user Deleted', life: 3000 });
              })
            }
        });
    }
 }
