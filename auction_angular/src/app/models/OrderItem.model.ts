import { Order } from "./Order.model";
import { Present } from "./Present.model";

export class OrderItem
{
    id:number=0;
    orderId?:number;
    presentId: number=0;
    present:Present=new Present;
    quantity:number=1;
    status:boolean=false;
}