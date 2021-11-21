import { Component } from '@angular/core';
import { first } from 'rxjs/operators';

import { User } from '@app/_models';

import { UserService } from '@app/_services';
import { environment } from '@environments/environment';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    todoitemlist: any[];

    public todoitem = {
        Id:'',
        title: '',
        description: '',
        ModifedDate: '',
        IsSelected :false
      };
      submitted= false;


    constructor(private userService: UserService) { }

    ngOnInit() {
        this.loading = true;
       this.loadData();
    }

    loadData():void{
        this.userService.getAll().pipe(first()).subscribe(data => {
            debugger
            this.loading = false;
            this.todoitemlist = data;
          
        });
    }

    saveitem(): void {
     
        const data = {
            title: this.todoitem.title,
            description: this.todoitem.description
          };
        this.userService.create(data)
          .subscribe(
            response => {
              console.log(response);
              this.submitted = true;
              this.loadData();
            },
            error => {
              console.log(error);
            });
      }

      delete(st):void{
          this.userService.delete(st)
          .subscribe(
            response => {
              console.log(response);
              this.loadData();
             
            },
            error => {
              console.log(error);
            });
      }

      update(st):void{
          debugger
        this.userService.update(st)
        .subscribe(
          response => {
            console.log(response);
            this.loadData();
           
          },
          error => {
            console.log(error);
          });
    }

    clear():void
    {
        this.todoitem.description='';
    }
}