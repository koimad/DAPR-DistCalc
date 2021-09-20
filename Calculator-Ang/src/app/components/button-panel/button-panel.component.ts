import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'ButtonPanel',
  templateUrl: './button-panel.component.html',
  styleUrls: ['./button-panel.component.scss']
})
export class ButtonPanelComponent implements OnInit {

    @Output() buttonPressed: EventEmitter<string> = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  OnPressed(buttonName: string) {
    this.buttonPressed.emit(buttonName);
    
  }

}
