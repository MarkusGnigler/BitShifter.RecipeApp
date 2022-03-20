import { Component, Input } from '@angular/core';

@Component({
	selector: 'ui-not-found',
	template: `
		<!-- <section class="section">
			<p>{{ text }}</p>
			<mat-icon>report</mat-icon>
		</section> -->
		<mat-card class="card">
			<mat-card-content class="content">
				<p>{{ text }}</p>
				<mat-icon>report</mat-icon>
			</mat-card-content>
		</mat-card>
	`,
	styles: [
		`
			:host {
				--icon-size: 45px;

				height: 100%;
				display: flex;
				flex-direction: column;
				justify-content: center;
				align-items: center;
			}

			.section,
			mat-card-content {
				display: flex;
				justify-content: center;
				flex-direction: column;
				align-items: center;
			}

			.mat-icon {
				height: var(--icon-size);
				width: var(--icon-size);
				font-size: var(--icon-size);
			}
		`,
	],
})
export class NotFoundComponent {
	//
	@Input() text!: string;
}
