
### Standard example usage
```TS
@Component({
	selector: 'ui-sidenav-layout',
	template: `
		<!-- PORTAL -->
		<ui-portal elementSelector="#sidenav-action-buttons">
			<div fxLayout="column" fxFlexAlign="center center">
				<p>Footer</p>
			</div>
		</ui-portal>

		<ui-sidenav [navItems]="navItems">
			<!-- <ui-notification></ui-notification> -->
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