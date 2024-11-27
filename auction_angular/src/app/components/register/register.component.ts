import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageService } from 'primeng/api';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from 'src/app/models/User.model';
import { UserDTO } from 'src/app/models/UserDTO.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(private router: Router, private activatedRoute: ActivatedRoute,private userService :UsersService,private messageService:MessageService){}
  email:string='';
  firstName:string='';
  lastName:string='';
  password:string='';
  user = new User;
  userD=new UserDTO;
  register() {
    this.user.email = this.email;
    this.user.firstName = this.firstName;
    this.user.lastName = this.lastName;
    this.user.password = this.password;
  
    this.userService.addUser(this.user).pipe(
      catchError((error) => {
        if (error.status === 400) { 
          this.router.navigate(['./register/', {relativeTo: this.activatedRoute}]);
        }
        return throwError(error);
      })
    ).subscribe((newUser) => {
      this.userD.Email = newUser.email;
      this.userD.password = newUser.password;
  
      this.userService.login(this.userD).pipe(
        catchError((error) => {
          if (error.status === 400) { 
            this.router.navigate(['./register/', {relativeTo: this.activatedRoute}]);
          }
          return throwError(error);
        })
      ).subscribe((token: string) => {
        if (this.userD.Email === "faigy@gmail.com" && this.userD.password === "7850") {
          sessionStorage.setItem('user', JSON.stringify(token));
          this.router.navigateByUrl('manage');
        } else if (token) {
          sessionStorage.setItem('user', JSON.stringify(token));
          this.router.navigate(['./raffle/' + this.userD.Email, {relativeTo: this.activatedRoute}]);
        }
  
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'User created', life: 3000 });
      });
      
      this.userService.setReloadUser();
      this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'User created', life: 3000 });
    });
  }
}
