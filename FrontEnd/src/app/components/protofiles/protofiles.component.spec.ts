import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProtofilesComponent } from './protofiles.component';

describe('ProtofilesComponent', () => {
  let component: ProtofilesComponent;
  let fixture: ComponentFixture<ProtofilesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProtofilesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProtofilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
