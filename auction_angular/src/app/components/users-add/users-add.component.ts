import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { MessageService } from 'primeng/api';
import { User } from 'src/app/models/User.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  styleUrls: ['./users-add.component.css']
})
export class UsersAddComponent {
  constructor(private userService: UsersService, private messageService: MessageService) { }

  @Input()
  user: User = new User();
  submitted: boolean = false;
  @Input()
  userDialog: boolean = true;
  @Output()
  userDialogChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  hideDialog() {
    this.userDialog = false;
    this.userDialogChange.emit(this.userDialog);
    this.submitted = false;
  }

  saveUser() {
    this.submitted = true;
    if (this.user.email.trim()) {

      if (this.user.userId) {
        this.userService.saveUser(this.user).subscribe(b => {
          this.userService.setReloadUser();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'user Updated', life: 3000 });
        });
      }
      else {  
        this.userService.addUser(this.user).subscribe(a => {   
          this.userService.setReloadUser();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'user Created', life: 3000 });
        });
      }
      this.userDialogChange.emit(this.userDialog);
      this.user = new User;
      this.hideDialog()
    }
  }

}
