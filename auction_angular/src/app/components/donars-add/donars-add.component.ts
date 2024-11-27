import { Component, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Donar } from 'src/app/models/Donar.model';
import { DonarsService } from 'src/app/services/donars.service';

@Component({
  selector: 'app-donars-add',
  templateUrl: './donars-add.component.html',
  styleUrls: ['./donars-add.component.css']
})
export class DonarsAddComponent {
  constructor(private donarService: DonarsService, private messageService: MessageService) { }



  @Input()
  donar: Donar = new Donar();
  submitted: boolean = false;
  @Input()
  donarDialog: boolean = true;
  @Output()
  donarDialogChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  hideDialog() {
    this.donarDialog = false;
    this.donarDialogChange.emit(this.donarDialog);
    this.submitted = false;
  }

  saveDonar() {
    this.submitted = true;
    if (this.donar.firstName.trim()&&this.donar.lastName.trim()) {

      if (this.donar.id) {
        this.donarService.saveDonar(this.donar).subscribe(b => {
          console.log(b,"a")   
          this.donarService.setReloadDonar();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'donar Updated', life: 3000 });
        });
      }
      else {  
        this.donarService.addDonar(this.donar).subscribe(a => {
          console.log(a,"a")    
          this.donarService.setReloadDonar();
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'donar Created', life: 3000 });
        });
      }
      this.donarDialogChange.emit(this.donarDialog);
      this.donar = new Donar;
      this.hideDialog()
    }
  }

}
