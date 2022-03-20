import { Component } from '@angular/core';
import { LayoutService } from '../../services/layout.service';

@Component({
	selector: 'ui-theme-container',
	template: `
		<article [class.light-theme]="isLightTheme$ | async">
			<main>
				<ng-content></ng-content>
			</main>
		</article>
	`,
	styles: [
		`
			article {
				height: 100%;
				color: #fff;
				background: #303030;
			}

			article.light-theme {
				color: #000;
				background: white;
			}
		`,
	],
})
export class ThemeContainerComponent {
	//
	isLightTheme$ = this.layoutService.isLightTheme$;

	constructor(private layoutService: LayoutService) {}
}
