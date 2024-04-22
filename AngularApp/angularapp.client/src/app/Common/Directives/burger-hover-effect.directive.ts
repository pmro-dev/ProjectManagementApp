import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2, SimpleChanges } from '@angular/core';

@Directive({
  selector: '[appBurgerHoverEffect]'
})

export class BurgerHoverEffectDirective {

  constructor(private renderer: Renderer2) { }

  @Input() outerElem?: ElementRef;
  private isShowed: boolean = false;
  @Output() isShowedOutput = new EventEmitter<boolean>();
  @Input() isShowedIn: boolean;

  ngOnChanges(changes: SimpleChanges): void {

    if (changes['isShowedIn'] && changes['isShowedIn'].previousValue !== undefined) {
      if (this.isShowed) {
        this.show();
      }
      else {
        this.hide();
      }
    }
  }

  hide() {
    this.renderer.setStyle(this.outerElem?.nativeElement, 'transform', `translateX(-100%)`);
    this.isShowed = false;
    this.isShowedOutput.emit(this.isShowed);
  }

  show() {
    this.renderer.setStyle(this.outerElem?.nativeElement, 'transform', `translateX(0%)`);
    this.isShowed = true;
    this.isShowedOutput.emit(this.isShowed);
  }

  @HostListener('click') click(eventData: Event) {
    if (this.isShowed) {
      this.hide();
    }
    else {
      this.show();
    }
  }
}