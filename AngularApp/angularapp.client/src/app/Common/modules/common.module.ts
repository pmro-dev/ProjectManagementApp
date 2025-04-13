import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavLeftMenuComponent } from '../components/navigations/sidebar/nav-left-menu/nav-left-menu.component';
import { NavTopMenuComponent } from '../components/navigations/top-menu/nav-top-menu/nav-top-menu.component';
import { BurgerHoverEffectDirective } from '../directives/burger-hover-effect.directive';

@NgModule({
  declarations: [
    BurgerHoverEffectDirective,
    NavLeftMenuComponent,
    NavTopMenuComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    BurgerHoverEffectDirective,
    NavLeftMenuComponent,
    NavTopMenuComponent
  ]
})

export class CommonModuleModule { }
