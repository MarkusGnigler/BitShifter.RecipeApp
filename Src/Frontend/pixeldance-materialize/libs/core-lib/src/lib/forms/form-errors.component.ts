import { Component, Inject, Input } from '@angular/core';
import { AbstractControl } from '@angular/forms';
import { FORM_ERRORS } from './form-errors';

@Component({
	selector: 'pxd-form-errors',
	template: `
		<mat-error *ngFor="let error of errorArray">
			<span *ngIf="control?.hasError(error)">
				{{ errorMessage(error) }}
			</span>
		</mat-error>
	`,
})
export class FormErrorsComponent {
	//
	// eslint-disable-next-line  @typescript-eslint/no-explicit-any
	errorArray!: any[];

	@Input() control!: AbstractControl;

	// eslint-disable-next-line  @typescript-eslint/no-explicit-any
	constructor(@Inject(FORM_ERRORS) public errors: any) {
		this.errorArray = Object.keys(this.errors);
	}

	errorMessage(errorName: string): string {
		const getError = this.errors[errorName];
		return getError(this.control?.errors);
	}
}
