
### Standard example usage
```TS
@Component({
	selector: 'ui-sidenav-layout',
	template: `
		<!-- PORTAL -->
		<pxd-portal elementSelector="#sidenav-action-buttons">
			<div fxLayout="column" fxFlexAlign="center center">
				<p>Footer</p>
			</div>
		</pxd-portal>

		<ui-sidenav [navItems]="navItems">
			<!-- <pxd-notification></pxd-notification> -->
			<section class="section" fxLayout="column" fxFlexAlign="center center">
				<router-outlet></router-outlet>
			</section>
		</ui-sidenav>
	`,
	styles: [
		`
			.section {
				padding: 25px;
			}
		`,
	],
})
export class SidenavLayoutComponent {
	//
	navItems: NavItem[] = [
		{ name: 'Seite 1', route: 'page1' },
		{ name: 'Seite 2', route: 'page2', disabled: false },
	];
}

```