import { Component } from '@angular/core';
import { BusyService } from './busy.service';

@Component({
	selector: 'ui-loading-spinner',
	template: `
		<div *ngIf="busyService.isActive" class="container">
			<mat-spinner></mat-spinner>
		</div>
	`,
	styles: [
		`
			:host {
				--cube-size: 150px;
				--primeColor: #004857;
			}

			.container {
				z-index: 99;
				position: absolute;
				top: 0;
				bottom: 0;
				right: 0;
				left: 0;

				pointer-events: none;
				background-color: rgba(0, 0, 0, 0.2);

				display: flex;
				justify-content: center;
				align-items: center;
				box-sizing: border-box;
			}
		`,
	],
})
export class LoadingSpinnerComponent {
	//
	constructor(public busyService: BusyService) {}
}
