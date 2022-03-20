
### Standard example usage
```TS
@Component({
	selector: 'ui-topbar-layout',
	template: `
		<!-- HEADER RIGHT -->
		<ng-template #navHeaderLeftTemplate>
			<p>Header left</p>
		</ng-template>

		<!-- HEADER MIDDLE -->
		<ng-template #navHeaderMiddleTemplate>
			<p>Header middle</p>
		</ng-template>

		<!-- HEADER MIDDLE -->
		<ng-template #navHeaderRightTemplate>
			<p>Header right</p>
		</ng-template>

		<!-- TOPBAR -->
		<ui-topbar
			[navHeaderLeftTemplate]="navHeaderLeftTemplate"
			[navHeaderMiddleTemplate]="navHeaderMiddleTemplate"
			[navHeaderRightTemplate]="navHeaderRightTemplate"
		></ui-topbar>
	`,
	styles: [],
})
export class TopbarLayoutComponent { }

```