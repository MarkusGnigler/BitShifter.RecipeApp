import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input } from '@angular/core';

@Component({
  selector: 'core-form-error',
  template: `
    <mat-error [class.hide]="_hide">
      {{_text}}
    </mat-error>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FormErrorComponent {

  _text!: string;
  _hide = true;

  @Input() set text(value: string) {
    if (value !== this._text) {
      this._text = value;
      this._hide = !value;
      this.cdr.detectChanges();
    }
  };

  constructor(private cdr: ChangeDetectorRef) { }

}
