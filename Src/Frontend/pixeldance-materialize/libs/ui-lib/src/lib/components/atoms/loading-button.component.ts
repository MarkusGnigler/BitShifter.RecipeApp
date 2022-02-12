import { AfterContentInit, ChangeDetectionStrategy, ContentChild, ViewChild } from '@angular/core';
import { Component, Input, TemplateRef } from '@angular/core';
import { timer } from 'rxjs';

export const BUTTON_CONTENT = 'buttonContent';

@Component({
	selector: 'ui-loading-button',
	template: `
		<ng-template #loading>
			<img src="https://github.com/alcfeoh/ng-advanced-workshop/raw/master/src/assets/loader.gif" style="width: 20px" />
			<!-- <div class="lds-ellipsis"><div></div><div></div><div></div><div></div></div> -->
		</ng-template>

		<button mat-raised-button [color]="color" (click)="triggerAction()">
			<ng-container *ngTemplateOutlet="currentTemplate"></ng-container>
		</button>
	`,
	styles: [
		`
			.lds-ellipsis {
				display: inline-block;
				position: relative;
				/* width: 80px;
      height: 80px; */
			}
			.lds-ellipsis div {
				position: absolute;
				/* top: 33px; */
				width: 13px;
				height: 13px;
				border-radius: 50%;
				background: #fff;
				animation-timing-function: cubic-bezier(0, 1, 1, 0);
			}
			.lds-ellipsis div:nth-child(1) {
				left: 8px;
				animation: lds-ellipsis1 0.6s infinite;
			}
			.lds-ellipsis div:nth-child(2) {
				left: 8px;
				animation: lds-ellipsis2 0.6s infinite;
			}
			.lds-ellipsis div:nth-child(3) {
				left: 32px;
				animation: lds-ellipsis2 0.6s infinite;
			}
			.lds-ellipsis div:nth-child(4) {
				left: 56px;
				animation: lds-ellipsis3 0.6s infinite;
			}
			@keyframes lds-ellipsis1 {
				0% {
					transform: scale(0);
				}
				100% {
					transform: scale(1);
				}
			}
			@keyframes lds-ellipsis3 {
				0% {
					transform: scale(1);
				}
				100% {
					transform: scale(0);
				}
			}
			@keyframes lds-ellipsis2 {
				0% {
					transform: translate(0, 0);
				}
				100% {
					transform: translate(24px, 0);
				}
			}
		`,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoadingButtonComponent implements AfterContentInit {
	//
	action$ = timer(2000);

	@Input() color = 'primary';

	@Input() initialTemplate!: TemplateRef<HTMLElement>;

	@ViewChild('loading') loadingContainer!: TemplateRef<HTMLElement>;

	@ContentChild(BUTTON_CONTENT, { static: false })
	buttonContent!: TemplateRef<HTMLElement>;

	currentTemplate!: TemplateRef<HTMLElement>;

	get buttonTemplate() {
		return this.initialTemplate ?? this.buttonContent;
	}

	ngAfterContentInit(): void {
		this.currentTemplate = this.buttonTemplate;
	}

	triggerAction() {
		this.currentTemplate = this.loadingContainer;
		this.action$.subscribe(() => (this.currentTemplate = this.buttonTemplate));
	}
}
