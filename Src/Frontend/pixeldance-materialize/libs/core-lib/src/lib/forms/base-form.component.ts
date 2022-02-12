import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';

@Component({
	selector: 'pxd-base-form',
	template: '',
})
export abstract class BaseFormComponent implements OnInit {
	//
	formGroup!: FormGroup;

	ngOnInit(): void {
		this.formGroup = this.buildFormGroup();
	}

	public getField = (name: string): FormControl => this.formGroup.get(name) as FormControl;
	public getArray = (name: string, formGroup?: FormGroup) => (formGroup ?? this.formGroup).get(name) as FormArray;

	protected buildFormGroup(): FormGroup {
		throw new Error('You have to implement the "buildFormGroup()" method!');
	}

	protected clearFormArray(formArray: FormArray) {
		while (formArray.length !== 0) formArray.removeAt(0);
	}

	private getErrorMessage() {
		if (this.getField('username')?.hasError('required')) return 'You must enter a username';
		if (this.getField('password')?.hasError('required')) return 'You must enter a password';

		return this.getField('username')?.hasError('email') ? 'Not a valid email' : '';
	}
}
