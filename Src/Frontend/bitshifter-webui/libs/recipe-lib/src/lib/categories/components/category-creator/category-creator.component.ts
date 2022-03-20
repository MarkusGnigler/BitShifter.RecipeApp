import { Component, ElementRef, EventEmitter, Output, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseFormComponent } from '@bitshifter-webui/core-lib';
import { Category } from '../../models/category';

@Component({
	selector: 'bs-category-creator',
	templateUrl: './category-creator.component.html',
	styleUrls: ['./category-creator.component.scss'],
})
export class CategoryCreatorComponent extends BaseFormComponent {
	//
	@ViewChild('') private categorySearch: ElementRef<HTMLElement> | undefined = undefined;

	@Output() createCategory = new EventEmitter<Category>();

	override buildFormGroup(): FormGroup {
		return new FormGroup({
			categoryName: new FormControl('', [Validators.required]),
		});
	}
	
	onSubmit() {
		this.createCategory.emit(this.buildCategory());
		this.categorySearch?.nativeElement.blur();
		this.formGroup.reset();
	}

	private buildCategory(): Category {
		return {
			id: '',
			name: this.getField('categoryName')?.value ?? null,
			isEditable: false,
		};
	}
}
