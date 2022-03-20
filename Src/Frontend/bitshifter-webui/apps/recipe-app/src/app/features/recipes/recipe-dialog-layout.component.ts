import { Component } from '@angular/core';

@Component({
	selector: 'bs-recipe-dialog-layout',
	template: `
		<article fxLayout fxLayoutAlign="center center">
			<router-outlet></router-outlet>
		</article>
	`,
	styles: [
		`
			article {
				width: 100%;
				padding: 45px;
			}
		`,
	],
})
export class RecipeDialogLayoutComponent {}
