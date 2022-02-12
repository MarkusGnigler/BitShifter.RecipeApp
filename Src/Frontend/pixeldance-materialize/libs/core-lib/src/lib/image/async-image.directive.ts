import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[pxdAsyncImage]'
})
export class AsyncImageDirective {

  constructor( { nativeElement }: ElementRef<HTMLImageElement> ) {
    const supports = 'decoding' in HTMLImageElement.prototype;
    if (supports) nativeElement.setAttribute('decoding', 'async');
  }

}
