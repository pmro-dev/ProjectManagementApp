import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BurgerHoverEffectDirective } from '../Directives/burger-hover-effect.directive';
import { NavLeftMenuComponent } from '../Components/Navigations/Sidebar/nav-left-menu/nav-left-menu.component';
import { NavTopMenuComponent } from '../Components/Navigations/Top-Menu/nav-top-menu/nav-top-menu.component';

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
