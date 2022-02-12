import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormErrorsDirective } from './dynamic/directives/form-errors.directive';
import { FormSubmitDirective } from './dynamic/directives/form-submit.directive';
import { FormErrorComponent } from './dynamic/form-error.component';
import { FormErrorsComponent } from './form-errors.component';
import { FormArraySelectorComponent } from './components/form-array-selector.component';
import { MatIconModule } from '@angular/material/icon';

const externModules = [
	FormsModule,
	ReactiveFormsModule
]

const directives = [
	FormErrorsDirective,
	FormSubmitDirective,
];

const components = [
	FormErrorComponent, 
	FormErrorsComponent,
    FormArraySelectorComponent
];

@NgModule({
	declarations: [directives, components],
	imports: [CommonModule, MatFormFieldModule, MatIconModule, externModules],
	exports: [
		directives,
		components,
		externModules
	],
})
export class PxdFormsModule {}
