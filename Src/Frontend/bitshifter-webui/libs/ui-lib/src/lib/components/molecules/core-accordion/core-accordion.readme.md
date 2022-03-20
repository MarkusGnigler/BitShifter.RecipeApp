
## Example usage simple
```html
<ui-core-accordion [items]="items">
	<ng-template #itemTemplate let-item let-id="index">
		<div>
			{{ item | json }} : {{ id }}
		</div>
	</ng-template>
</ui-core-accordion>
```

# Example usage with custom header
```html
<ng-template #actionTemplate let-item>
		<button mat-icon-button color="primary">
			<mat-icon>library_books</mat-icon>
		</button>
</ng-template>

<ui-core-accordion [items]="items" [actionTemplate]="actionTemplate">
	<ng-template #itemTemplate let-item let-id="index">
		<div>{{ item | json }} : {{ id }}</div>
	</ng-template>
</ui-core-accordion>
```


