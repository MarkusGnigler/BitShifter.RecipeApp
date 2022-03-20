import { Component, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { LayoutService } from '../../services/layout.service';

export const TOP_HEADER_LEFT_TEMPLATE = 'navHeaderLeftTemplate';
export const TOP_HEADER_MIDDLE_TEMPLATE = 'navHeaderMiddleTemplate';
export const TOP_HEADER_RIGHT_TEMPLATE = 'navHeaderRightTemplate';

@Component({
	selector: 'ui-topbar',
	templateUrl: './topbar.component.html',
})
export class TopbarComponent {
	//
	@Output() toggleSidenav = new EventEmitter();

	@Input(TOP_HEADER_LEFT_TEMPLATE)
	navHeaderLeftTemplate!: TemplateRef<HTMLElement>;

	@Input(TOP_HEADER_MIDDLE_TEMPLATE)
	navHeaderMiddleTemplate!: TemplateRef<HTMLElement>;

	@Input(TOP_HEADER_RIGHT_TEMPLATE)
	navHeaderRightTemplate!: TemplateRef<HTMLElement>;

	// @ContentChild(TOP_HEADER_LEFT_TEMPLATE, { static: false })
	// topHeaderLeftTemplate!: TemplateRef<HTMLElement>;

	constructor(public layoutService: LayoutService) {}
}
