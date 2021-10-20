
import { HttpClientTestingModule,HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { ButtonComponent } from './components/button/button.component';
import { CalculateService } from './services/calculate.service';
import { StateService } from './services/state.service';
import { of } from 'rxjs';
import { ButtonPanelComponent } from './components/button-panel/button-panel.component';
import { DisplayComponent } from './components/display/display.component';


describe('AppComponent', () => {

  let calculateService: jasmine.SpyObj<CalculateService>;
  let stateService: jasmine.SpyObj<StateService>;

  beforeEach(async () => {

    stateService = jasmine.createSpyObj<StateService>(['getState']);
    calculateService = jasmine.createSpyObj<CalculateService>(['calculate']);
   
    TestBed.configureTestingModule({
      declarations: [
        AppComponent, ButtonPanelComponent, DisplayComponent, ButtonComponent
      ],
      imports: [HttpClientTestingModule],
      providers: [
        { provide: CalculateService, useValue: calculateService },
        { provide: StateService, useValue: stateService },
      ]
    }).compileComponents();
  });

  it('should create the app', async () => {
    
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });


  it('Initial Value should be 0 with no state set', async () => {

    stateService.getState.and.returnValue(of({}).toPromise());

    const fixture = TestBed.createComponent(AppComponent);

    fixture.detectChanges();

    const app = fixture.componentInstance;

    fixture.whenStable().then(() => {
      expect(app.value).toBe("0");
    });
  });


  it('Initial Value should be populated from total when next is not set', async () => {

    stateService.getState.and.returnValue(of({ total: "12", next: null, operation: null }).toPromise());

    const fixture = TestBed.createComponent(AppComponent);

    fixture.detectChanges();

    const app = fixture.componentInstance;

    fixture.whenStable().then(() => {
      expect(app.value).toBe("12");
    });
  });


  it('Initial Value should be populated from next when next is set', async () => {

    stateService.getState.and.returnValue(of({ total: "12", next: "99", operation: null }).toPromise());

    const fixture = TestBed.createComponent(AppComponent);

    fixture.detectChanges();

    const app = fixture.componentInstance;

    fixture.whenStable().then(() => {
      expect(app.value).toBe("99");
    });

  });
});
