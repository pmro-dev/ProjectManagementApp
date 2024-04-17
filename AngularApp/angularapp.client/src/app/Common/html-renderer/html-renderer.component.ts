import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'common-html-renderer',
  templateUrl: './html-renderer.component.html',
  styleUrl: './html-renderer.component.css',
  standalone: true
})

export class HtmlRendererComponent implements OnChanges{
  @Input() customContent: SafeHtml;

  constructor(private sanitizer: DomSanitizer) { }

  ngOnChanges(changes: SimpleChanges) {
    if ('customContent' in changes) {
      const newContent = changes['customContent'].currentValue;
      this.customContent = this.sanitizer.bypassSecurityTrustHtml(newContent.innerHTML);
    }
  }
}