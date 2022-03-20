import { Component, OnDestroy, OnInit } from '@angular/core';
import { LayoutService } from '../services/layout.service';

@Component({
	selector: 'ui-base-view',
	template: `
		TEMPLATE COULDN'T INHERITED
	`,
})
export class BaseViewComponent implements OnInit, OnDestroy {
	//
	protected titleText!: string;

	constructor(protected layoutService: LayoutService) {}

	ngOnDestroy(): void {
		this.layoutService.setTopbarTitle('');
	}

	ngOnInit(): void {
		this.layoutService.setTopbarTitle(this.titleText);
	}
}
