import { Component } from '@angular/core';
import { BaseResolver } from '@bitshifter-webui/core-lib';
import { BaseRecipeDialogComponent } from '../base-recipe-dialog.component';

@Component({
	selector: 'bs-recipe-editor',
	templateUrl: './recipe-editor.component.html',
	styles: [
		`
			:host {
				width: 100%;
			}
		`,
	],
})
export class RecipeEditorComponent extends BaseRecipeDialogComponent implements BaseResolver {
	//
	
	// eslint-disable-next-line @angular-eslint/use-lifecycle-interface
	override ngOnInit(): void {
		this.resolveRoute();
		super.ngOnInit();
	}

	resolveRoute() {
		this.route.data.subscribe(data => {
			this.resolvedRecipe = data['recipe'];
		});
	}
}
