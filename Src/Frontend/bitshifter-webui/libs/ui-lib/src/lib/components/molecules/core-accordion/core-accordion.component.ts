import { Component, ContentChild, Input, TemplateRef } from '@angular/core';

const ITEM_TEMPLATE = 'itemTemplate';
const ACTION_TEMPLATE = 'actionTemplate';

@Component({
	selector: 'ui-core-accordion',
	templateUrl: './core-accordion.component.html',
	styleUrls: ['./core-accordion.component.scss'],
})
export class CoreAccordionComponent<T extends { title: string }> {
	//
	@Input() items!: T[];

	@Input(ACTION_TEMPLATE)
	actionTemplate!: TemplateRef<HTMLElement>;

	@ContentChild(ITEM_TEMPLATE, { static: true })
	itemTemplateRef!: TemplateRef<HTMLElement>;
}
