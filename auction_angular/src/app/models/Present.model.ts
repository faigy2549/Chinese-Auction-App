import { Donar } from "./Donar.model";
import { User } from "./User.model";

export class Present
{
    presentId: number=0;
    name: string='';
    donarId: number=0;
    donar?:any;
    category: number=0;
    price: number=50;
    image_Url:string='';
    pcount:number=0;
    winner?:string;
}