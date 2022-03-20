import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

@Injectable({
	providedIn: 'root',
})
export class NavigationService {
	//
	private history: string[] = [];

	get History(): string[] {
		return this.history;
	}

	constructor(private router: Router, private location: Location) {
		// this.router.events
		// 	.pipe(
		// 		filter(event => event instanceof NavigationEnd),
		// 		map(event => event as NavigationEnd),
		// 	)
		// 	.subscribe((event: NavigationEnd) => {
		// 		console.log('end rout', event.urlAfterRedirects);
		// 		this.history = [...this.history, event.urlAfterRedirects];
		// 	});
	}

	back(): void {
		// this.history.pop();
		// if (this.history.length > 0) {
		// 	this.location.back();
		// 	return;
		// }
		// this.router.navigateByUrl('/');
	}

	public getPreviousUrl(): string {
		return this.history[this.history.length - 1] || '/index';
	}
}
