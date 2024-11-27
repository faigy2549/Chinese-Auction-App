import { Component, Input, ViewChild } from '@angular/core';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { SelectItem } from 'primeng/api';
import { Table, TableModule } from 'primeng/table';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Present } from 'src/app/models/Present.model';
import { PresentService } from 'src/app/services/present.service';
import { switchMap, tap, forkJoin } from 'rxjs';
import { DonarsService } from 'src/app/services/donars.service';
@Component({
  selector: 'app-present-list',
  templateUrl: './present-list.component.html',
  styleUrls: ['./present-list.component.css'],
})
export class PresentListComponent {
    searchValue: string = "";
@ViewChild('dt') dt!:Table;
  listDonars:string []=["Bil gates","Rothchild","Elan musk","Inside","miri`s","blew","peek a boo","ronit","Chana Vatury"];
  presentDialog: boolean = false;

    presents: Present[] = [];

    present: Present = new Present();

    selectedPresents!: Present[];

    submitted!: boolean;

    constructor(public donarService: DonarsService,public presentService: PresentService, private messageService: MessageService, private confirmationService: ConfirmationService) { 
        this.dt = {} as Table;
    }

    ngOnInit() {
      this.presentService.reloadPresents$.pipe(
        switchMap(() => this.presentService.getPresents("reg")),
        switchMap(presents => {
          this.presents = presents;
          const donarObservables = this.presents.map(element =>
            this.donarService.getDonarById(element.donarId).pipe(
              tap(data => {
                element.donar = data;
              })
            )
          );
          return forkJoin(donarObservables);
        })
        ).subscribe();
    }

    openNew() {
        this.present = new Present();
        this.submitted = false;
        this.presentDialog = true;
    }
  
    applyFilter() {
        if (this.dt) {
          this.dt.reset(); 
          this.dt.filterGlobal(this.searchValue, 'contains');
        }
      }

    editPresent(present: Present) {
        this.present = { ...present };
        this.presentService.getPresentById(this.present.presentId).subscribe(present => this.present = present)
        this.presentDialog = true;
    }

    deletePresent(present: Present) {
        this.confirmationService.confirm({
            message: 'Are you sure you want to delete ' + present.name + '?',
            header: 'Confirm',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {          
                this.presentService.deletePresent(present.presentId).subscribe(c=>{
                this.presentService.setReloadPresent();
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'present Deleted', life: 3000 });
              })
            }
        });
    }
}
