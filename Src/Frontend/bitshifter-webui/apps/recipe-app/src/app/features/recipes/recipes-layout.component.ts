import { Component } from '@angular/core';

@Component({
	selector: 'bs-recipes-layout',
	template: `
		<article fxLayout="row" fxLayoutAlign="center center">
			<router-outlet></router-outlet>
		</article>
	`,
	styles: [
		`
			article {
				padding: 45px 10px;
                overflow-x: hidden;
                overflow-y: auto;
			}
		`,
	],
})
export class RecipesLayoutComponent {}
