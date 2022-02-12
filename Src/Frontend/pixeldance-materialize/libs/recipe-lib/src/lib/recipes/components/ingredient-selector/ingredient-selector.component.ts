import { Component, forwardRef, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, FormArray, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { Ingredient } from '../../models/ingredient';

export const INGREDIENT_FORMGROUP_NAMES = {
	title: 'title',
	quantity: 'quantity',
	unit: 'unit',
};

@Component({
	selector: 'pxd-ingredient-selector',
	templateUrl: './ingredient-selector.component.html',
	styleUrls: ['./ingredient-selector.component.scss'],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => IngredientSelectorComponent),
			multi: true,
		},
	],
})
export class IngredientSelectorComponent implements ControlValueAccessor, OnInit {
	//
	readonly FORMGROUP_NAMES = INGREDIENT_FORMGROUP_NAMES;

	flexGap = '32px';

	@Input() group?: FormGroup;

	formGroup?: FormGroup;

	get ingredients(): FormArray {
		return this.group?.get('ingredients') as FormArray;
	}
	addIngredient = () => this.ingredients.push(this.buildIngredientFormGroup());
	removeIngredient = (index: number) => this.ingredients.removeAt(index);

	/* eslint-disable @typescript-eslint/member-ordering */
	value?: Ingredient;
	isDisabled?: boolean;
	/* eslint-disable @typescript-eslint/no-empty-function */
	private onChange: (value: Record<string, unknown> | string) => void = () => {};
	/* eslint-disable @typescript-eslint/no-empty-function */
	private onTouched: () => void = () => {};

	writeValue(obj: Ingredient): void {
		this.value = obj;
	}

	registerOnChange(fn: () => void): void {
		this.onChange = fn;
	}

	registerOnTouched(fn: () => void): void {
		this.onTouched = fn;
	}

	setDisabledState(isDisabled: boolean): void {
		this.isDisabled = isDisabled;
	}

	ngOnInit() {
		const ingredientsArray = this.ingredients?.value
			? this.ingredients?.value.map(this.buildIngredientFormGroup)
			: [this.buildIngredientFormGroup()];

		this.formGroup = new FormGroup({
			ingredients: new FormArray(ingredientsArray),
		});
	}

	protected buildIngredientFormGroup(ingredient?: Ingredient): FormGroup {
		return new FormGroup({
			title: new FormControl(ingredient?.title ?? '', [Validators.required]),
			quantity: new FormControl(ingredient?.quantity ?? 0, [Validators.required]),
			unit: new FormControl(ingredient?.unit ?? '', [Validators.required]),
		});
	}

	getControlByName = (name: string): FormControl =>
		this.ingredients.controls
			.map(x => x as FormGroup)
			.find(x => x.get(name))
			?.get(name) as FormControl;

	getErrorMessage(control?: FormControl) {
		if (!control) return '';

		if (control.hasError('required')) {
			return 'Du must einen Wert eingeben';
		}

		return control.hasError('required') ? 'Du must einen Wert eingeben' : '';
	}
}
