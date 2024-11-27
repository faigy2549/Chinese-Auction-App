import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import { Present } from "src/app/models/Present.model";
export function doablesValidator(listpres:Present[],n:boolean): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      let c=0;
      if(control.value && listpres && n==true){
       for(c=0;c<listpres.length;c++)
      {
        if(listpres[c].name==control.value){
            return {doable: {value: control.value}} 
        }
      }
    }
      return null;
    };
  }
  