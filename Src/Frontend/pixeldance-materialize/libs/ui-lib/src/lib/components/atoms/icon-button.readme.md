
### example
```ts
@Component({
	selector: 'ui-icon-button',
	template: `
		<ng-container>
			<ui-icon-button 
				icon="add" 
				side="right" 
				[enabled]="true" 
				(clicked)="onIconButtonClicked()"
			>
			</ui-icon-button>
			
			<ui-icon-button 
				icon="add" 
				side="left" 
				[enabled]="true" 
				(clicked)="onIconButtonClicked()"
			>
			</ui-icon-button>
		</ng-container>
	`,
	styles: [],
})
export class IconButtonComponent {
	//
	onIconButtonClicked() {
		console.log('onIconButtonClicked');
	}
}
```