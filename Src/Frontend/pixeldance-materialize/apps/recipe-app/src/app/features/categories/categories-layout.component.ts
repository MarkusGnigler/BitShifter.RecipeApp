import { Component } from '@angular/core';

@Component({
	selector: 'pxd-categorie-layout',
	template: `
		<article fxLayout fxLayoutAlign="center center">
			<section>
				<router-outlet></router-outlet>
			</section>
		</article>
	`,
	styles: [
		`
			section {
				width: 100%;
				padding: 45px;
			}
		`,
	],
})
export class CategoriesLayoutComponent {}
