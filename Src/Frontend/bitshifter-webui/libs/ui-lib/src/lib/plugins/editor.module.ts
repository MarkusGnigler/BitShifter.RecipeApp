import { NgModule } from '@angular/core';
import { NgxEditorModule } from 'ngx-editor';
// import { AngularEditorModule } from '@kolkov/angular-editor';

const configuration = {
	locals: {
		// menu
		bold: 'Bold',
		italic: 'Italic',
		code: 'Code',
		underline: 'Underline',
		strike: 'Strike',
		blockquote: 'Blockquote',
		bullet_list: 'Bullet List',
		ordered_list: 'Ordered List',
		heading: 'Heading',
		h1: 'Header 1',
		h2: 'Header 2',
		h3: 'Header 3',
		h4: 'Header 4',
		h5: 'Header 5',
		h6: 'Header 6',
		align_left: 'Left Align',
		align_center: 'Center Align',
		align_right: 'Right Align',
		align_justify: 'Justify',
		text_color: 'Text Color',
		background_color: 'Background Color',
		insertLink: 'Insert Link',
		removeLink: 'Remove Link',
		insertImage: 'Insert Image',

		// pupups, forms, others...
		url: 'URL',
		text: 'Text',
		openInNewTab: 'Open in new tab',
		insert: 'Insert',
		altText: 'Alt Text',
		title: 'Title',
		remove: 'Remove',
	},
};

@NgModule({
	// imports: [AngularEditorModule],
	// exports: [AngularEditorModule],
	imports: [NgxEditorModule.forRoot(configuration)],
	exports: [NgxEditorModule],
})
export class EditorModule {}
