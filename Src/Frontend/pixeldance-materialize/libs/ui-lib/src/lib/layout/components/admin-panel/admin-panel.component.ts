import { Component, Input, TemplateRef, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { NavItem } from '../../models/nav-item';
import { LayoutService } from '../../services/layout.service';
import { TOP_HEADER_MIDDLE_TEMPLATE, TOP_HEADER_RIGHT_TEMPLATE } from '../topbar/topbar.component';

export const NAV_HEADER_TEMPLATE = 'navHeaderTemplate';
export const NAV_FOOTER_TEMPLATE = 'navFooterTemplate';
export const UPPER_FOOTER_BUTTONS = 'upperFooterTemplate';

@Component({
	selector: 'ui-admin-panel',
	templateUrl: './admin-panel.component.html',
})
export class AdminPanelComponent {
	//
	@Input() navItems!: NavItem[];

	@Input(NAV_HEADER_TEMPLATE)
	navHeaderTemplate!: TemplateRef<HTMLElement>;

	@Input(NAV_FOOTER_TEMPLATE)
	navFooterTemplate!: TemplateRef<HTMLElement>;

	@Input(TOP_HEADER_MIDDLE_TEMPLATE)
	navHeaderMiddleTemplate!: TemplateRef<HTMLElement>;

	@Input(TOP_HEADER_RIGHT_TEMPLATE)
	navHeaderRightTemplate!: TemplateRef<HTMLElement>;

	@Input(UPPER_FOOTER_BUTTONS)
	upperFooterTemplate!: TemplateRef<HTMLElement>;

	@ViewChild(MatSidenav) sidenav!: MatSidenav;

	isExpanded = true;
	isMouseOverNav = false;

	isLightTheme$ = this.layoutService.isLightTheme$;
	isScreenSmall$ = this.layoutService.isScreenSmall$;

	toggleButtons!: string;

	get isMiniNav() {
		return !this.isExpanded;
	}

	constructor(private layoutService: LayoutService) {}

	onMouseOver() {
		if (this.isExpanded) return;
		this.isMouseOverNav = true;
		this.isExpanded = true;
	}
	onMouseOut() {
		if (!this.isMouseOverNav) return;
		this.isMouseOverNav = false;
		this.isExpanded = false;
	}

	onButtonGroupClicked() {
		console.log(`Tooglebutton state => ${this.toggleButtons}`);

		let isLightTheme = this.toggleButtons === 'dark';
		isLightTheme = this.toggleButtons === 'light';

		this.layoutService.setTheme(isLightTheme);
		if (this.toggleButtons === undefined) this.isExpanded = !this.isExpanded;
	}

	onToggleTheme() {
		this.layoutService.toggleTheme();
	}

	onToggleSideBar() {
		this.isExpanded = !this.isExpanded;
	}
}
