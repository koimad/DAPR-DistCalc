import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'Display',
  templateUrl: './display.component.html',
  styleUrls: ['./display.component.scss']
})
export class DisplayComponent implements OnInit {

  @Input()
  value: string = "";

  constructor() { }

  ngOnInit(): void {
  }

}
