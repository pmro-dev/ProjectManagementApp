import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appBurgerHoverEffect]'
})
export class BurgerHoverEffectDirective {

  constructor(private elemRef: ElementRef, private renderer: Renderer2) { }

  @Input() outerElem?: HTMLDivElement;
  private isShowed: boolean = false;

  ngAfterViewInit() {
    if (!this.outerElem) {
      console.error('outerElem is not provided!');
      return;
    }

    this.outerElem = this.outerElem;
    this.isShowed = false;
  }

  @HostListener('click') click(eventData: Event) {
    if (this.isShowed) {
      this.renderer.setStyle(this.outerElem, 'transform', `translateX(-100%)`);
      this.isShowed = false;
    }
    else {
      this.renderer.setStyle(this.outerElem, 'transform', `translateX(0%)`);
      this.isShowed = true;
    }
  }
}