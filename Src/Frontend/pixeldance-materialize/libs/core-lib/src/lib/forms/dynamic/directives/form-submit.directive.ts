import { Directive, ElementRef } from '@angular/core';
import { fromEvent, Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';

@Directive({
  // Selector is valid for general all forms
  // eslint-disable-next-line  @angular-eslint/directive-selector
  selector: 'form'
})
export class FormSubmitDirective {

  submit$: Observable<Event> = fromEvent( this.element, 'submit' ).pipe( shareReplay(1) )

  constructor( private host: ElementRef<HTMLFormElement> ) { }

  get element() {
    return this.host.nativeElement
  }

}
