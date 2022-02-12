import { Component } from '@angular/core';

@Component({
	selector: 'acr-stand-alone-layout',
	template: `
		<ui-theme-container>
			<!-- SIDENAV-HEADER -->
			<ng-template #navHeaderLeftTemplate>
				<!-- <img src="./assets/images/company-logo.jpg" style="width: 133px" /> -->
			</ng-template>

			<!-- TOPBAR -->
			<ui-topbar [navHeaderLeftTemplate]="navHeaderLeftTemplate"></ui-topbar>

			<!-- NOTIFICATION -->
			<!-- <masp-notification></masp-notification> -->

			<!-- CONTENT -->
			<router-outlet></router-outlet>
		</ui-theme-container>
	`,
})
export class StandAloneLayoutComponent {}
