import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
	selector: 'ui-icon-button',
	template: `
		<button
			mat-fab
			color="primary"
			class="add-button"
			[disabled]="!enabled"
			(click)="clicked.emit()"
			[class.left]="side === 'left'"
			[class.right]="side === 'right'"
		>
			<mat-icon>{{ icon }}</mat-icon>
		</button>
	`,
	styles: [
		`
			:host {
				--iconOffset: 32px;
			}
			button {
				--iconOffset: 32px;
				position: absolute;
				bottom: var(--iconOffset);
			}
			.left {
				left: var(--iconOffset);
			}
			.right {
				right: var(--iconOffset);
			}
		`,
	],
})
export class IconButtonComponent {
	//
	@Input() enabled = true;
	@Input() icon = 'add';
	@Input() side: 'left' | 'right' = 'right';

	@Output() clicked = new EventEmitter<boolean>();
}
