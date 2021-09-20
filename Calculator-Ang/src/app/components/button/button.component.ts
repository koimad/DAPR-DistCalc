import { Component, EventEmitter, HostBinding, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'CalcButton',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit {

  constructor() { }

  @Input() name: string = '';

  @Input() highlight: boolean = false;

  @Input()
  @HostBinding('class.wider') wide: boolean = false;

  @Output() buttonPressed: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {

  }
  OnPressed() {
    this.buttonPressed.emit(this.name);
  }
}
