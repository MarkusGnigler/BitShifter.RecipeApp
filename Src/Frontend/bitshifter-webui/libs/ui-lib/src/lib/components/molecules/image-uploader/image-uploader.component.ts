import { Component, forwardRef, Input } from '@angular/core';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
	selector: 'ui-image-uploader',
	templateUrl: './image-uploader.component.html',
	styleUrls: ['./image-uploader.component.scss'],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => ImageUploaderComponent),
			multi: true,
		},
	],
})
export class ImageUploaderComponent implements ControlValueAccessor {
	//
	@Input() parentForm?: FormGroup;
	@Input() fieldName?: string;
	@Input() label = 'Wähle ein Bild';

	value!: File;
	changed!: (value: File) => void;
	touched!: () => void;
	isDisabled!: boolean;

	get formField(): FormControl {
		return this.parentForm?.get(this.fieldName ?? '') as FormControl;
	}

	writeValue(obj: File): void {
		this.value = obj;
	}
	registerOnChange(fn: () => void): void {
		this.changed = fn;
	}
	registerOnTouched(fn: () => void): void {
		this.touched = fn;
	}

	setDisabledState(isDisabled: boolean): void {
		this.isDisabled = isDisabled;
	}

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	onFileSelected(event: any) {
		if (event.target?.files.length < 1) return;

		const file = event.target.files[0] as File;

		if (file == null) return;
		if (file.name.lastIndexOf('.') <= 0) return;

		this.value = file;
		this.changed(file);
	}
}
