import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BurgerHoverEffectDirective } from '../Directives/burger-hover-effect.directive';

@NgModule({
  declarations: [
    BurgerHoverEffectDirective
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BurgerHoverEffectDirective
  ]
})
export class CommonModuleModule { }
