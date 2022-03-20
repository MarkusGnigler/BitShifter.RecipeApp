import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

export const DIALOG_ACTIONS_TEMPLATE = 'dialogActionsTemplate';

@Component({
	selector: 'ui-base-dialog',
	template: `
		<div
			matDialogTitle
			class="dialog-header"
			cdkDrag
			cdkDragBoundary=".cdk-overlay-container"
			cdkDragRootElement=".cdk-overlay-pane"
			cdkDragHandle
		>
			<ng-container *ngIf="colored; else notColoredTemplate">
				<mat-toolbar color="primary" [class.pl]="disableClose">
					<h2 class="header">{{ title }}</h2>
					<button mat-icon-button [mat-dialog-close] *ngIf="!disableClose">
						<mat-icon>close</mat-icon>
					</button>
				</mat-toolbar>
			</ng-container>
			<ng-template #notColoredTemplate>
				<section [class.pl]="disableClose">
					<h2 class="header">{{ title }}</h2>
					<button mat-icon-button [mat-dialog-close] *ngIf="!disableClose">
						<mat-icon>close</mat-icon>
					</button>
				</section>
			</ng-template>
		</div>

		<mat-dialog-content class="mat-typography" cdkFocusRegionStart>
			<ng-content></ng-content>
		</mat-dialog-content>

		<mat-dialog-actions *ngIf="dialogActionsTemplate">
			<ng-container [ngTemplateOutlet]="dialogActionsTemplate"></ng-container>
		</mat-dialog-actions>
	`,
	styles: [
		`
			:host {
				--dialogHeaderOffset: 24px;
				--dialogHeaderOffset--: calc(-1 * var(--dialogHeaderOffset));
			}

			.dialog-header {
				margin-top: var(--dialogHeaderOffset--);
				margin-left: var(--dialogHeaderOffset--);
				margin-right: var(--dialogHeaderOffset--);
				margin-bottom: var(--dialogHeaderOffset);
			}

			h2 {
				font-size: 1.15rem;
				font-weight: 600;
			}

			section {
				width: 100%;
				display: flex;
				align-items: center;
				margin: 0px 24px;
			}

			.header {
				width: 100%;
				text-align: center;
			}

			mat-toolbar:not(.pl),
			section:not(.pl) {
				padding-left: 30px !important;
			}

			/* .pl {
				padding-left: 30px !important;
			} */

			.mat-dialog-container {
				min-width: 25rem;
			}

			/* https://stackoverflow.com/questions/51379525/angular-5-material-dialog-box-with-header-strip-on-top */
			/* .mat-dialog-container {
				padding-top: 0 !important;
			}

			dialog-overview-example-dialog.ng-star-inserted > div {
				margin-right: -24px;
				margin-left: -24px;
			}

			.mat-dialog-actions {
				margin-right: 0 !important;
				margin-left: 0 !important;
			}

			.mat-dialog-title {
				margin-top: 15px !important;
			} */
		`,
	],
})
export class BaseDialogComponent implements OnInit {
	//
	@Input() colored = true;
	@Input() disableClose = false;

	@Input() title!: string;

	@Input(DIALOG_ACTIONS_TEMPLATE)
	dialogActionsTemplate!: TemplateRef<HTMLElement>;

	constructor(public dialogRef: MatDialogRef<never>) {}

	ngOnInit(): void {
		this.dialogRef.disableClose = this.disableClose;
	}
}
