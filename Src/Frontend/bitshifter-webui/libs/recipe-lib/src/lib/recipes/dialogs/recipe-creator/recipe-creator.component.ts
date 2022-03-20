import { Component } from '@angular/core';
import { BaseRecipeDialogComponent } from '../base-recipe-dialog.component';

@Component({
	selector: 'bs-recipe-creator',
	templateUrl: './recipe-creator.component.html',
	styles: [
		`
			:host {
				width: 100%;
			}
		`,
	],
})
export class RecipeCreatorComponent extends BaseRecipeDialogComponent {}
