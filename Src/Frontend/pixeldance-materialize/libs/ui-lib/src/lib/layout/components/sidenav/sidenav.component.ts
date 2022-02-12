import { Component, Input, TemplateRef } from '@angular/core';
import { NavItem } from '../../models/nav-item';
import { LayoutService } from '../../services/layout.service';

const NAV_HEADER_TEMPLATE = 'navHeaderTemplate';

@Component({
	selector: 'ui-sidenav',
	templateUrl: './sidenav.component.html',
})
export class SidenavComponent {
	//
	@Input() navItems!: NavItem[];

	@Input(NAV_HEADER_TEMPLATE)
	navHeaderTemplate!: TemplateRef<HTMLElement>;

	isLightTheme$ = this.layoutService.isLightTheme$;

	constructor(private layoutService: LayoutService) {}
}
