import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'nav-left-menu',
  templateUrl: './nav-left-menu.component.html',
  styleUrl: './nav-left-menu.component.css'
})

export class NavLeftMenuComponent {
  @Output() PushMenuToParent = new EventEmitter<ElementRef>();
  @ViewChild('mobileMenu', { static: true }) mobileMenuViaChild: ElementRef;

  constructor(private elementRef: ElementRef) {}

  mobileMenu: ElementRef;

  ngOnInit() {
    this.mobileMenu = this.elementRef.nativeElement.querySelector('div');
    this.PushMenuToParent.emit(this.mobileMenuViaChild);
  }
}
