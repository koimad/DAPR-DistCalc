import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ButtonComponent } from '../button/button.component';

import { ButtonPanelComponent } from './button-panel.component';

describe('ButtonPanelComponent', () => {
  let component: ButtonPanelComponent;
  let fixture: ComponentFixture<ButtonPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ButtonPanelComponent, ButtonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ButtonPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
