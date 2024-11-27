import { Component, ViewChild } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Donar } from 'src/app/models/Donar.model';
import { Present } from 'src/app/models/Present.model';
import { DonarsService } from 'src/app/services/donars.service';

@Component({
  selector: 'app-donars',
  templateUrl: './donars.component.html',
  styleUrls: ['./donars.component.css']
})
export class DonarsComponent {
    @ViewChild('dt') dt!:Table;
    searchValue: string = "";
  donarDialog: boolean = false;

  donars: Donar[] = [];

    donar: Donar = new Donar();

    selectedDonars!: Donar[];

    submitted!: boolean;

    constructor(public donarService: DonarsService, private messageService: MessageService, private confirmationService: ConfirmationService) { }

    ngOnInit() {
        this.donarService.reloadDonars$.subscribe(x => {
            this.donarService.getDonars().subscribe(data => this.donars = data);
        });
    }

    openNew() {
        this.donar = new Donar();
        this.submitted = false;
        this.donarDialog = true;
    }
  
    applyFilter() {
        if (this.dt) {
          this.dt.reset(); 
          this.dt.filterGlobal(this.searchValue, 'contains');
        }
      }

    editDonar(donar: Donar) {
        this.donar = { ...donar };
        this.donarService.getDonarById(this.donar.id).subscribe(donar => this.donar = donar)
        this.donarDialog = true;
    }

    deleteDonar(donar: Donar) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + donar.firstName + " "+donar.lastName+'?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {          
                this.donarService.deleteDonar(donar.id).subscribe(c=>{
                this.donarService.setReloadDonar();
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'donar Deleted', life: 3000 });
              })
            }
        });
    }
    getPresentNames(presents: Present[]): string[] {
      return presents.map(present => present.name);
    }
}
