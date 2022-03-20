import { Component, forwardRef, OnDestroy } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Editor } from 'ngx-editor';
// import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
	selector: 'ui-html-editor',
	templateUrl: './html-editor.component.html',
	styleUrls: ['./html-editor.component.scss'],
	providers: [
		{
			provide: NG_VALUE_ACCESSOR,
			useExisting: forwardRef(() => HtmlEditorComponent),
			multi: true,
		},
	],
})
export class HtmlEditorComponent implements ControlValueAccessor, OnDestroy {
	//
	editor: Editor;

	htmlContent = new FormControl('');

	// editorConfig: AngularEditorConfig = {
	// 	editable: true,
	// 	  spellcheck: true,
	// 	  height: 'auto',
	// 	  minHeight: '0',
	// 	  maxHeight: 'auto',
	// 	  width: 'auto',
	// 	  minWidth: '0',
	// 	  translate: 'yes',
	// 	  enableToolbar: true,
	// 	  showToolbar: true,
	// 	  placeholder: 'Enter text here...',
	// 	  defaultParagraphSeparator: '',
	// 	  defaultFontName: '',
	// 	  defaultFontSize: '',
	// 	  fonts: [
	// 		{class: 'arial', name: 'Arial'},
	// 		{class: 'times-new-roman', name: 'Times New Roman'},
	// 		{class: 'calibri', name: 'Calibri'},
	// 		{class: 'comic-sans-ms', name: 'Comic Sans MS'}
	// 	  ],
	// 	  customClasses: [
	// 	  {
	// 		name: 'quote',
	// 		class: 'quote',
	// 	  },
	// 	  {
	// 		name: 'redText',
	// 		class: 'redText'
	// 	  },
	// 	  {
	// 		name: 'titleText',
	// 		class: 'titleText',
	// 		tag: 'h1',
	// 	  },
	// 	],
	// 	uploadUrl: 'v1/image',
	// 	// upload: (file: File) => { },
	// 	uploadWithCredentials: false,
	// 	sanitize: true,
	// 	toolbarPosition: 'top',
	// 	toolbarHiddenButtons: [
	// 	  ['bold', 'italic'],
	// 	  ['fontSize']
	// 	]
	// };

	constructor() {
		this.editor = new Editor();
	}

	writeValue(value: string) {
		if (!value) return;
		this.htmlContent.setValue(value);
	}

	registerOnChange(fn: () => void): void {
		this.htmlContent.valueChanges.subscribe(fn);
	}

	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	registerOnTouched(fn: () => void): void {
		// console.log('registerOnTouched');
	}

	setDisabledState(isDisabled: boolean): void {
		if (isDisabled) return this.htmlContent.enable();
		this.htmlContent.disable();
	}

	ngOnDestroy(): void {
		this.editor.destroy();
	}
}
