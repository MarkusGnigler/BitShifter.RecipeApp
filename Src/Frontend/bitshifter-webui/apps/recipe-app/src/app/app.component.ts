import { Component } from '@angular/core';

@Component({
	selector: 'bs-root',
	template: `
		<router-outlet></router-outlet>
		<!-- <bs-app-layout></bs-app-layout> -->
	`,
})
export class AppComponent {}
