import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ButtonComponent } from './button.component';

describe('ButtonComponent', () => {
  let component: ButtonComponent;
  let fixture: ComponentFixture<ButtonComponent>;


  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ButtonComponent ]
    })
      .compileComponents();
    fixture = TestBed.createComponent(ButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });


  it('should emit event when clicked', () => {

    component.name = "123";

    let invokeName = "";

    const subscription = component.buttonPressed.subscribe(f => { invokeName = f })

    component.OnPressed();

    expect(invokeName).toBe(component.name);

    subscription.unsubscribe();
  });

});
