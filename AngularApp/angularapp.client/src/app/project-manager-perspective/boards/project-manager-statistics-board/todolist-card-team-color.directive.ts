import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appTodolistCardTeamColor]'
})
export class TodolistCardTeamColorDirective {

  constructor(private elemRef: ElementRef, private renderer: Renderer2) { }

  @Input() teamColor: string = 'rgb(247, 247, 247)';

  ngOnInit(): void {
    this.renderer.setStyle(this.elemRef.nativeElement, 'background-color', this.teamColor);
  }
}
