import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GalleryContentComponent } from './gallery-content.component';

describe('GalleryContentComponent', () => {
  let component: GalleryContentComponent;
  let fixture: ComponentFixture<GalleryContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GalleryContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GalleryContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
