import { Component, ElementRef, EventEmitter, Input, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'nav-top-menu',
  templateUrl: './nav-top-menu.component.html',
  styleUrl: './nav-top-menu.component.css'
})

export class NavTopMenuComponent {
  public appLogoPath: string = "/assets/other/appLogo.jpg";
  public currentUserName: string = "Jan Kowalski";
  public userAvatarPath: string = "/assets/avatars/avatar1-mini.jpg";
  @Input('mobileMenuIn') mobileMenu: ElementRef;
  @Output() isMenuShowed = new EventEmitter<boolean>();
  @Input() mobileMenuShowIn: boolean;
  isMenuShowStatus: boolean;

  ngOnChanges(changes: SimpleChanges): void {

    if (changes['mobileMenuShowIn'] && changes['mobileMenuShowIn'].previousValue !== undefined) {
      this.isMenuShowStatus = this.mobileMenuShowIn;
    }
  }

  onMenuShowChange(isShowed: boolean) {
    this.isMenuShowStatus = isShowed;
    this.isMenuShowed.emit(this.isMenuShowStatus);
  }
}
