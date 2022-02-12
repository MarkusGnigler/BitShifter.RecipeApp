import { ChangeDetectionStrategy, Component, forwardRef, Input, OnDestroy } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete/autocomplete';
import { Subject } from 'rxjs';
import { debounceTime, filter, map, startWith, take, takeUntil, tap } from 'rxjs/operators';

@Component({
	selector: 'ui-autocomplete-filter',
	templateUrl: './autocomplete-filter.component.html',
	styleUrls: ['./autocomplete-filter.component.scss'],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => AutocompleteFilterComponent),
			multi: true,
		},
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AutocompleteFilterComponent<T extends { toString(): string }> implements ControlValueAccessor, OnDestroy {
	//
	@Input() label = 'Filter';
	@Input() possibleOptions: T[] = [];

	changed!: (value: T) => void;

	private readonly destroyed$ = new Subject();

	readonly autoCompleteControl = new FormControl();

	private readonly _filteValue = new Subject<string | null>();
	readonly filteredOptions$ = this._filteValue.pipe(
		startWith(null),
		debounceTime(500),
		map((option: string | null) => (option ? this._filter(option) : this.possibleOptions)),
	);

	private _filter(value: string): T[] {
		const filterValue = value.toString().toLowerCase();

		return this.possibleOptions.filter(option => option.toString().toLowerCase().includes(filterValue)) ?? [];
	}

	// eslint-disable-next-line @typescript-eslint/member-ordering
	private readonly registerGetStringValueForFilter = this.autoCompleteControl.valueChanges.pipe(
		filter(x => typeof x === 'string'),
		tap(x => this._filteValue.next(x)),
		takeUntil(this.destroyed$),
	);

	constructor() {
		this.registerGetStringValueForFilter.subscribe();
	}

	ngOnDestroy(): void {
		this.destroyed$.next(null);
	}

	writeValue(value: T): void {
		this.autoCompleteControl.setValue(value);
	}
	registerOnChange(fn: () => void): void {
		this.changed = fn;
	}
	registerOnTouched(fn: () => void): void {
		this.autoCompleteControl.statusChanges.pipe(take(1)).subscribe(fn);
	}
	setDisabledState(isDisabled: boolean): void {
		isDisabled ? this.autoCompleteControl.disable() : this.autoCompleteControl.enable();
	}

	onUserInput(event: Event): void {
		const value = (event.target as HTMLInputElement).value;
		this._filteValue.next(value);
	}

	onOptionSelected(event: MatAutocompleteSelectedEvent) {
		this.changed(event.option.value as T);
	}
}
