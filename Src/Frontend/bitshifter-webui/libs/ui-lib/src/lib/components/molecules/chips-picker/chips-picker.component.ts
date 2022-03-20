import { Component, ElementRef, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatChip, MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
	selector: 'ui-chips-picker',
	templateUrl: './chips-picker.component.html',
	styleUrls: ['./chips-picker.component.scss'],
})
export class ChipsPickerComponent<T extends { toString(): string }> {
	//
	@Input() selectable = false;
	@Input() removable = true;

	@Input() title!: string;
	
	get placeholder(): string {
		return `${this.title} ...`;
	}

	@Input() options!: T[];
	@Input() possibleOptions!: T[];

	@Output() addOption = new EventEmitter<T>();
	@Output() removeOption = new EventEmitter<T>();
	@Output() toggleOption = new EventEmitter<MatChip>();

	@ViewChild('optionInput') optionInput!: ElementRef<HTMLInputElement>;

	optionControl = new FormControl();
	filteredOptions$!: Observable<T[]>;
	separatorKeysCodes: number[] = [ENTER, COMMA];

	constructor() {
		this.filteredOptions$ = this.optionControl.valueChanges.pipe(
			startWith(null),
			map((option: string | null) => (option ? this._filter(option) : this.possibleOptions)),
		);
	}

	toggleSelection(chip: MatChip) {
		if (!this.selectable) return;
		chip.toggleSelected();
		this.toggleOption.emit(chip);
	}

	addChip(event: MatChipInputEvent): void {
		const input = event.input;
		const value = event.value as never;
		if (!input) return;

		input.value = '';
		this.addOption.emit(value);
		this.optionControl.setValue('');
	}

	onOptionSelected(event: MatAutocompleteSelectedEvent): void {
		this.optionInput.nativeElement.value = '';
		this.optionControl.setValue('');
		this.addOption.emit(event.option.value);
	}

	private _filter(value: string): T[] {
		const filterValue = value.toString().toLowerCase();

		return this.possibleOptions.filter(option => option.toString().toLowerCase().indexOf(filterValue) === 0);
	}
}
