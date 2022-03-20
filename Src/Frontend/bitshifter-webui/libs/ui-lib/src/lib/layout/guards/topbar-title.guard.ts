import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { LayoutService } from '../services/layout.service';

@Injectable({
	providedIn: 'root',
})
export class TopbarTitleGuard implements CanActivate {
	//
	constructor(private layoutService: LayoutService) {}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
		const title = this.getTitleFromRoute(state.url);
		this.layoutService.setTopbarTitle(title);
		return true;
	}

	private getTitleFromRoute(text: string) {
		return this.toCase(text.substr(1, text.length));
	}

	private toCase(text: string) {
		return text.replace(/\w\S*/g, function (txt) {
			return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();
		});
	}
}
