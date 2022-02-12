import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Output, EventEmitter, Input, ViewChild, NgZone } from '@angular/core';
import { take } from 'rxjs/operators';

@Component({
	selector: 'ui-textarea-autogrow',
	template: `
		<textarea
			matInput
			cdkTextareaAutosize
			#autosize="cdkTextareaAutosize"
			[cdkAutosizeMinRows]="minRows"
			[cdkAutosizeMaxRows]="maxRows"
			(input)="input($event)"
		></textarea>
	`,
})
export class TextareaAutogrowComponent {
	//
	@ViewChild('autosize')
	autosize!: CdkTextareaAutosize;

	@Input() minRows = 1;
	@Input() maxRows = 8;

	@Output() inputChanged = new EventEmitter<string>();

	constructor(private _ngZone: NgZone) {}

	// eslint-disable-next-line @typescript-eslint/no-explicit-any
	input(event: any) {
		this.inputChanged.emit(event.target.value);
	}
	triggerResize() {
		// Wait for changes to be applied, then trigger textarea resize.
		this._ngZone.onStable.pipe(take(1)).subscribe(() => this.autosize.resizeToFitContent(true));
	}
}
