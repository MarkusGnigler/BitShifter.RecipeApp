import { Component, ContentChild, Input, TemplateRef } from '@angular/core';
import { NavItem } from '../../models/nav-item';

export const ITEM_TEMPLATE = 'itemTemplate';

@Component({
	selector: 'ui-tab-bar',
	templateUrl: './tab-bar.component.html',
	styles: [
		`
			:host {
				width: 100%;
			}
		`,
	],
})
export class TabBarComponent {
	@Input() navItems!: NavItem[];

	@ContentChild(ITEM_TEMPLATE, { static: false })
	itemTemplateRef!: TemplateRef<HTMLElement>;
}
