import { Component, EventEmitter, HostBinding, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'CalcButton',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit {

  constructor() { }

  @Input() name: string = '';

  get testComponentName() { return `Calc-Button-${this.name}`;  }

  @Input() public highlight: boolean = false;

  @Input()
  @HostBinding('class.wider') public wide: boolean = false;

  @Output() public buttonPressed: EventEmitter<string> = new EventEmitter<string>();

  ngOnInit(): void {

  }
  OnPressed() {
    this.buttonPressed.emit(this.name);
  }
}
