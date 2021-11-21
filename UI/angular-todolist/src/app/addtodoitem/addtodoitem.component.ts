import { Component, OnInit } from '@angular/core';
import { UserService } from '@app/_services';

@Component({
  selector: 'app-addtodoitem',
  templateUrl: './addtodoitem.component.html',
  styleUrls: ['./addtodoitem.component.less']
})
export class AddtodoitemComponent implements OnInit {

  todoitem = {
    title: '',
    description: '',
    ModifedDate: '',
    IsSelected :false
  };


  constructor() { }

  ngOnInit(): void {
  }

}
