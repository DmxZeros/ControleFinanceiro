import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListagenGanhosComponent } from './listagen-ganhos.component';

describe('ListagenGanhosComponent', () => {
  let component: ListagenGanhosComponent;
  let fixture: ComponentFixture<ListagenGanhosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListagenGanhosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListagenGanhosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
