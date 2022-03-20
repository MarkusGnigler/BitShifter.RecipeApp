import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterializeModule } from '../plugins/materialize.module';
import { CoreLibModule } from '@bitshifter-webui/core-lib';
import { IconButtonComponent } from './atoms/icon-button.component';
import { LoadingButtonComponent } from './atoms/loading-button.component';
import { AutocompleteFilterComponent } from './molecules/autocomplete-filter/autocomplete-filter.component';
import { ChipsPickerComponent } from './molecules/chips-picker/chips-picker.component';
import { BaseDialogComponent } from './organisms/base-dialog.component';
import { CoreTableComponent } from './molecules/core-table/core-table.component';
import { FormFieldComponent } from './molecules/form-field.component';
import { ToDataSourcePipe } from './molecules/core-table/to-data-source.pipe';
import { NotFoundComponent } from './atoms/not-found.component';
import { CoreAccordionComponent } from './molecules/core-accordion/core-accordion.component';
import { SearchBoxComponent } from './atoms/search-box.component';
import { TextareaAutogrowComponent } from './atoms/textarea-autogrow.component';
import { CollapsableContainerComponent } from './atoms/collapsable-container.component';
import { HtmlEditorComponent } from './molecules/html-editor/html-editor.component';
import { ImageUploaderComponent } from './molecules/image-uploader/image-uploader.component';
import { EditorModule } from '../plugins/editor.module';

const components = [
	// Atoms
	CollapsableContainerComponent,
	LoadingButtonComponent,
	IconButtonComponent,
	NotFoundComponent,
	SearchBoxComponent,
	TextareaAutogrowComponent,
	// Molecules
	AutocompleteFilterComponent,
	ChipsPickerComponent,
	CoreTableComponent,
	ToDataSourcePipe,
	FormFieldComponent,
	CoreAccordionComponent,
	HtmlEditorComponent,
	ImageUploaderComponent,
	// Organisms
	BaseDialogComponent,
];

@NgModule({
	declarations: [components],
	exports: [components],
	imports: [CommonModule, CoreLibModule, MaterializeModule, EditorModule],
})
export class ComponentsModule {}
