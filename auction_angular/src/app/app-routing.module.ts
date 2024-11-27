import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PresentListComponent } from './components/present-list/present-list.component';

import { DonarsComponent } from './components/donars/donars.component';
import { RaffleComponent } from './components/raffle/raffle.component';
import { ManageComponent } from './components/manage/manage.component';
import { UsersComponent } from './components/users/users.component';
import { PaymentComponent } from './components/payment/payment.component';
import { CartComponent } from './components/cart/cart.component';
import { MainComponent } from './components/login/main.component';
import { RegisterComponent } from './components/register/register.component';


 const routes: Routes = [
  {path:'',component:MainComponent},
  {path:'main',component:MainComponent},
  {path:'manage', component:ManageComponent},
  {path:'presents', component:PresentListComponent},
{path:'donars', component:DonarsComponent},
{path:'raffle/:userName', component:RaffleComponent},
{path:'payment/:total', component:PaymentComponent},
{path:'cart/:userName', component:CartComponent},
{path:'users', component:UsersComponent},
{path:'register', component:RegisterComponent}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

