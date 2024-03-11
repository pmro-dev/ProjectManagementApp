import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appTeamCardHighlight]'
})
export class TeamCardHighlightDirective {

  constructor(private elemRef: ElementRef, private renderer: Renderer2) { }

  @Input() borderColor: string = 'rgb(247, 247, 247)';
  @Input() defaultBorderColor: string = 'rgb(247, 247, 247)';
  private cardColor: string = 'rgb(247, 247, 247)';

  ngOnInit(): void {
    let colorTemp: string = `${this.defaultBorderColor} ${this.defaultBorderColor} ${this.cardColor} ${this.cardColor}`;
    let shadowTemp: string = `4px -4px 9px -6px ${this.borderColor}, 4px -4px 9px -6px ${this.borderColor}`;
    this.renderer.setStyle(this.elemRef.nativeElement.querySelector('#cardCorner'), 'border-color', colorTemp);
    this.renderer.setStyle(this.elemRef.nativeElement.querySelector('#cardCorner'), 'box-shadow', shadowTemp);
  }

  @HostListener('mouseenter') mousehover(eventData: Event) {
    this.renderer.setStyle(this.elemRef.nativeElement.querySelector('#cardCorner'), 'opacity', `0.75`);
  }

  @HostListener('mouseleave') mouseleave(eventData: Event) {
    this.renderer.setStyle(this.elemRef.nativeElement.querySelector('#cardCorner'), 'opacity', '1');
  }
}
