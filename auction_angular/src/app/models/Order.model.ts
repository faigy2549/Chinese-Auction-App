import { OrderItem } from "./OrderItem.model";
import { Present } from "./Present.model";
import { User } from "./User.model";

export class Order
{
    orderId: number=0;
    userId:number=0;
    user:User=new User;
    date!:Date;
    sum:number=0;
    orderItems:OrderItem[]=[];
}