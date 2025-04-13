import { Directive, ElementRef, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appTodolistCardColor]'
})
export class TodolistCardColorDirective {

@Input() CardColor : string = "rgb(247, 247, 247)";

  constructor(private elemRef : ElementRef, private renderer : Renderer2) {}
  
  ngOnInit(): void {
    this.renderer.setStyle(this.elemRef.nativeElement, 'background-color', this.CardColor)
  }
}
