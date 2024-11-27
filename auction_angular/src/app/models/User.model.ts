import { Present } from "./Present.model";

export class User
{
    userId:number=0;
    email: string='';
    password:string='';
    firstName: string='';
    lastName: string='';
    Orders:Present[]=[];
    Role:number=1;
}