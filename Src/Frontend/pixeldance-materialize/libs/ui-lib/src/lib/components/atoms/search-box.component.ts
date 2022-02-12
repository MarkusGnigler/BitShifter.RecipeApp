import { Component, ElementRef, EventEmitter, HostListener, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';

@Component({
	selector: 'ui-search-box',
	template: `
		<mat-form-field appearance="fill">
			<mat-label>{{ label }}:</mat-label>
			<input #searchBox matInput [formControl]="searchCriteria" autocomplete="off" />
			<mat-icon matSuffix>search</mat-icon>
		</mat-form-field>
	`,
	styles: [
		`
			:host {
				width: 100%;
			}

			mat-form-field {
				width: 100%;
			}
		`,
	],
})
export class SearchBoxComponent implements OnInit {
	//
	@Input() label!: string;
	@Input() searchCriteria = new FormControl('');

	@Output() criteriaChanged = new EventEmitter<string>();

	@ViewChild('searchBox')
	private searchBox!: ElementRef<HTMLElement>;

	ngOnInit(): void {
		this.searchCriteria.valueChanges.pipe(debounceTime(1)).subscribe(criteria => this.criteriaChanged.emit(criteria));
	}

	@HostListener('click')
	onClick() {
		console.log('Component clicked');
	}

	@HostListener('focus')
	onFocus() {
		console.log('doooo');
		// this.setFocus();
	}

	setFocus() {
		console.log('Has focus', document.activeElement === this.searchBox.nativeElement);
		if (document.activeElement !== this.searchBox.nativeElement) {
			const timer = setTimeout(() => {
				this.searchBox.nativeElement.focus();
				clearTimeout(timer);
			}, 0);
		}
	}
}
