import { Component } from '@angular/core';
import { IdentityService } from '@pixeldance-materialize/identity-lib';
import { NavItem } from '@pixeldance-materialize/ui-lib';
import { environment } from '../environments/environment';

@Component({
	selector: 'pxd-app-layout',
	template: `
		<!-- SIDENAV-HEADER -->
		<ng-template #navHeaderTemplate>
			<!-- <img src="./assets/images/company-logo.jpg" style="width: 133px;" /> -->
			<h1>Rezepte App</h1>
		</ng-template>

		<!-- SIDENAV FOOTER -->
		<ng-template #navFooterTemplate>
			<a class="footer-link" href="https://www.pixeldance.at">pixeldance &copy; {{ currentYear }}</a>
		</ng-template>

		<!-- TOPBAR RIGHT HEADER -->
		<ng-template #topHeaderRightTemplate>
			<!-- USER -->
			<ng-container *ngIf="currentUser$ | async as currentUser; else elseTemplate">
				<p class="user-name">{{ currentUser.userName }}</p>

				<mat-icon class="avatar" [matMenuTriggerFor]="menu">account_circle</mat-icon>

				<button mat-icon-button [matMenuTriggerFor]="menu">
					<mat-icon>more_vert</mat-icon>
				</button>
				<mat-menu #menu="matMenu">
					<button mat-menu-item (click)="logout()">Logout</button>
				</mat-menu>
			</ng-container>
			<!-- NO USER -->
			<ng-template #elseTemplate>
				<mat-icon class="avatar" [matMenuTriggerFor]="menu">account_circle</mat-icon>

				<button mat-icon-button [matMenuTriggerFor]="menu">
					<mat-icon>more_vert</mat-icon>
				</button>
				<mat-menu #menu="matMenu">
					<button mat-menu-item routerLink="{{ loginPath }}">Login</button>
				</mat-menu>
			</ng-template>

			<!-- <ng-container>
				<p class="user-name">User</p>

				<mat-icon class="avatar" [matMenuTriggerFor]="menu">account_circle</mat-icon>

				<button mat-icon-button [matMenuTriggerFor]="menu">
					<mat-icon>more_vert</mat-icon>
				</button>
				<mat-menu #menu="matMenu">
					<button mat-menu-item routerLink="{{ loginPath }}">Login</button>
					<button mat-menu-item (click)="logout()">Logout</button>
				</mat-menu>
			</ng-container> -->
		</ng-template>

		<!-- ADMIN LAYOUT -->
		<ui-admin-panel
			[navItems]="navItems"
			[navHeaderTemplate]="navHeaderTemplate"
			[navFooterTemplate]="navFooterTemplate"
			[navHeaderRightTemplate]="topHeaderRightTemplate"
		>
			<router-outlet></router-outlet>
		</ui-admin-panel>
	`,
	styles: [
		`
			p {
				margin-right: 15px;
			}
			.avatar {
				cursor: pointer;
				width: 100% !important;
				height: 100% !important;
				font-size: 1.8em !important;
			}
			.mat-icon-button {
				font-size: 1rem !important;
			}
			a:link {
				text-decoration: none;
			}
		`,
	],
})
export class AppLayoutComponent {
	//
	currentYear = new Date().getFullYear();

	loginPath: string = environment.loginRoute;

	currentUser$ = this.identityService.currentUser$;

	navItems: NavItem[] = [
		{ name: 'Rezepte', route: 'rezepte', icon: 'admin_panel_settings' },
		{ name: 'Kategorien', route: 'kategorien', icon: 'reorder', disabled: false },
	];

	constructor(private identityService: IdentityService) {}

	logout() {
		//TODO
		this.identityService.logout();
		// this.identityService.logout().subscribe();
	}
}
