
### Standard example usage
```TS
@Component({
	selector: 'ui-tap-bar-layout',
	template: `
		<ui-tab-bar [navItems]="navItems">
			<ui-notification></ui-notification>
			<section fxLayout="column" fxFlexAlign="center center">
				<router-outlet></router-outlet>
			</section>
		</ui-tab-bar>
	`,
	styles: [
		`
			section {
				margin: 25px;
			}
		`,
	],
})
export class TapBarLayoutComponent {
	//
	navItems: NavItem[] = [
		{ name: 'Seite 1', route: 'page1' },
		{ name: 'Seite 2', route: 'page2', disabled: false },
	];
}
```