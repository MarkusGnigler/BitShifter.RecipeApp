import { Component, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { LayoutService } from '../../layout/services/layout.service';

@Component({
	selector: 'ui-collapsable-container',
	template: `
		<section class="buttons fx">
			<!-- <button mat-raised-button color="primary" (click)="onCollapse$.next()"> -->
			<button mat-raised-button color="primary" (click)="onClicked()">
				<ng-container *ngIf="(isCollapsed$ | async) === true; else elseTemplate">
					<mat-icon>expand_more</mat-icon>
				</ng-container>
				<ng-template #elseTemplate>
					<mat-icon>expand_less</mat-icon>
				</ng-template>
			</button>

			<button mat-raised-button color="primary" (click)="onClicked()">
				<ng-container *ngIf="isCollapsed$ | async; else elseTemplate">
					<mat-icon>expand_less</mat-icon>
				</ng-container>
				<ng-template #elseTemplate>
					<mat-icon>expand_more</mat-icon>
				</ng-template>
			</button>
		</section>
		<section class="container" *ngIf="(isCollapsed$ | async) === false">
			<div class="bg transparent"></div>
			<ng-content #name class="content"></ng-content>
		</section>
	`,
	styles: [
		`
			:host {
				bottom: 0;
				right: 0;
				left: 0;
				position: absolute;
				z-index: 999;
				pointer-events: none;
			}
			:host > *:not(.buttons) {
				pointer-events: auto;
			}

			/* Sets container z.b. upper tab-bar */
			/* @media screen and (max-width: 1100px) {
				ui-collapsable-container {
					bottom: 4.25rem !important;
				}
			} */

			.buttons {
				justify-content: space-between;
			}
			.buttons button {
				pointer-events: auto;
			}

			.container {
				bottom: 0;
				padding: 25px;
				position: relative;
			}

			.bg {
				position: absolute;
				top: 0;
				bottom: 0;
				right: 0;
				left: 0;
				background-color: rgba(0, 108, 131);
			}

			.transparent {
				-ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=25)'; /* IE 8 */
				filter: alpha(opacity=25); /* IE 5-7 */
				-moz-opacity: 0.25; /* Netscape */
				-khtml-opacity: 0.25; /* Safari 1.x */
				opacity: 0.25; /* Good browsers */
			}
		`,
	],
})
export class CollapsableContainerComponent implements OnDestroy {
	//
	private layoutSubscription: Subscription;

	private readonly _isCollapsed = new BehaviorSubject(true);
	readonly isCollapsed$ = this._isCollapsed;

	private readonly isScreenSmall$ = this.layoutService.isScreenSmall$;

	constructor(private layoutService: LayoutService) {
		this.layoutSubscription = this.isScreenSmall$.subscribe(x => this._isCollapsed.next(x));
	}

	ngOnDestroy(): void {
		this.layoutSubscription.unsubscribe();
	}

	onClicked() {
		this._isCollapsed.next(!this._isCollapsed.value);
	}
}
