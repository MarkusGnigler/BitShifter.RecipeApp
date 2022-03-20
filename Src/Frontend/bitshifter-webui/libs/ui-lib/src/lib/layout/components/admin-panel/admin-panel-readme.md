
### Example usage
```ts
@Component({
	selector: 'ui-layout',
	template: `
		<!-- SIDENAV-HEADER -->
		<ng-template #navHeaderTemplate>
			<img src="./assets/images/company-logo.jpg" style="width: 133px;" />
		</ng-template>

		<!-- SIDENAV FOOTER -->
		<ng-template #navFooterTemplate>
			<a class="footer-link" target="_blank" ref="https://www.bit-shifter.at">BitShifter &copy; {{ currentYear }}</a>
		</ng-template>

		<!-- TOPBAR MIDDLE HEADER -->
		<ng-template #navHeaderMiddleTemplate>
			<h1>Mitte</h1>
		</ng-template>

		<!-- TOPBAR RIGHT HEADER -->
		<ng-template #topHeaderRightTemplate>
			<!-- USER -->
			<ng-container *ngIf="currentUser$ | async as currentUser; else elseTemplate">
				<p class="user-name">
					({{ currentUser.erpNumber }})
					<span *ngIf="!(isScreenSmall$ | async)">- {{ currentUser.firstName }} {{ currentUser.lastName }}</span>
				</p>

				<mat-icon class="avatar" [matMenuTriggerFor]="menu">account_circle</mat-icon>

				<button mat-icon-button [matMenuTriggerFor]="menu">
					<mat-icon>more_vert</mat-icon>
				</button>
				<mat-menu #menu="matMenu">
					<button mat-menu-item>Abas an/abmelden</button>

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
		</ng-template>

        <!-- UPPER FOOTER -->
		<ng-template #upperFooterTemplate>
			<p>Upper footer</p>
		</ng-template>

		<!-- ADMIN LAYOUT -->
		<ui-admin-panel
			[navItems]="navItems"
			[navHeaderTemplate]="navHeaderTemplate"
			[navFooterTemplate]="navFooterTemplate"
			[navHeaderMiddleTemplate]="navHeaderMiddleTemplate"
			[navHeaderRightTemplate]="topHeaderRightTemplate"
			[upperFooterTemplate]="upperFooterTemplate"
		>
			<!-- <notify-notification></notify-notification> -->
			<router-outlet></router-outlet>
		</ui-admin-panel>
		<!-- SPINNER -->
		<ui-loading-spinner></ui-loading-spinner>
	`,
	styles: [
		`
			p {
				margin-right: 15px;
			}
			.avatar {
				cursor: pointer;
			}
			#btnEslFunctions {
				margin-top: auto !important;
				margin: 1rem;
				padding: 1rem;
				font-size: 0.7rem !important;
			}
		`,
	],
})
export class AcrLayoutComponent {
	//
	readonly loginPath: string = environment.LOGIN_ROUTE;

	readonly currentUser$ = this.identityService.currentUser$;

	readonly isScreenSmall$ = this.layoutService.isScreenSmall$;

	readonly currentYear = new Date().getFullYear();

	readonly navItems: NavItem[] = [
		{ name: 'Admin', route: 'admin/planung', icon: 'admin_panel_settings' },
		{ name: 'Auftragsansicht', route: 'order', icon: 'reorder' },
	];

	constructor(
		private router: Router,
		private layoutService: LayoutService,
		private identityService: IdentityService,
		private notificationService: NotificationService,
	) {}

	logout() {
		this.identityService.logout()?.subscribe({
			next: _ => this.router.navigate(['/identity/login']),
			error: _ => this.notificationService.showError(`Fehler bei der Abmeldung.`),
		});
	}
}

```