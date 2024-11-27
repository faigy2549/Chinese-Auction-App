import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from 'src/app/models/User.model';
import { UserDTO } from 'src/app/models/UserDTO.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {
  constructor(private router: Router, private activatedRoute: ActivatedRoute,private userService :UsersService,private messageService:MessageService){}
 
  userEmail:string='';
  password:string='';
  userDTO:UserDTO=new UserDTO;
  ngOnInit(): void {
    
  }
  login() {
    this.userDTO.password = this.password;
    this.userDTO.Email = this.userEmail;
    console.log("hgi");
    
    this.userService.login(this.userDTO).pipe(
      
      catchError((error) => {
        if (error.status === 400) { 
          console.log("fdf");
          
          this.router.navigate(['./register/', {relativeTo: this.activatedRoute}]);
        }
        return throwError(error);
      }),
      tap((token: string) => {
        console.log(token,"kkk");
        
        if (this.userDTO.Email === "f@admin.com" && this.userDTO.password === "7850") {
           sessionStorage.setItem('user', JSON.stringify(token));
          this.router.navigateByUrl('manage');
        } else if (token) {
          sessionStorage.setItem('user', JSON.stringify(token));
          this.router.navigate(['./raffle/' + this.userEmail, {relativeTo: this.activatedRoute}]);
        }
        
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'login successful', life: 3000 });
      })
    ).subscribe();
  }

register(){
    this.router.navigate(['./register/', {relativeTo: this.activatedRoute}]);
}

}
