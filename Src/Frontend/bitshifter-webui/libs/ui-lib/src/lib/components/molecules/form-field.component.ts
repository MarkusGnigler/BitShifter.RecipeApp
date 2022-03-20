import { Component, Input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
	selector: 'ui-form-field',
	template: `
		<mat-form-field *ngIf="formControl">
			<mat-label>{{ title }}</mat-label>
			<input matInput [formControl]="formControl" autocomplete="off" />
			<mat-error *ngIf="formControl!.invalid">{{ title }} wird benötigt</mat-error>
		</mat-form-field>
	`,
})
export class FormFieldComponent {
	//
	@Input() title!: string;
	@Input() formControl!: FormControl | null;
}
