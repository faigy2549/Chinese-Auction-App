import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/User.model';
import { UserDTO } from '../models/UserDTO.model';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private httpClient: HttpClient,private http: HttpClient,private router: Router,) { }
  private reloadUsersSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  reloadUsers$: Observable<boolean> = this.reloadUsersSubject.asObservable();

  getUsers(): Observable<User[]>{
    let url = 'https://localhost:44342/api/User/GetUsers';
    return this.httpClient.get<User[]>(url);
  }

  getUserById(id: number) : Observable<User>{
    let url = 'https://localhost:44342/api/User/user/' + id;
    return this.httpClient.get<User>(url);
  }

  saveUser(User: User) :Observable<boolean>{
    let url = 'https://localhost:44342/api/User/UpdateUser';
    return this.httpClient.put<boolean>(url, User); 
  }

  addUser(User: User) :Observable<User> {
    User.Role=1;
    let url = 'https://localhost:44342/api/User/register';
    return this.httpClient.post<User>(url, User)
  }
  login(userDTO:UserDTO) :Observable<string> {
    let url = 'https://localhost:44342/api/User/login';
    return this.httpClient.post<string>(url,userDTO);
  }
  deleteUser(number: number) :Observable<boolean> {
    let url = 'https://localhost:44342/api/User/deleteUser/'+number;
    return this.httpClient.delete<boolean>(url)
  }

   logout(): void {
     localStorage.removeItem('token');
     this.router.navigateByUrl('main');
   }


  setReloadUser(){
    let flag = this.reloadUsersSubject.value;
    this.reloadUsersSubject.next(!flag);
  }
}
