
### Example usage 1 with initial template
```html
    <!-- INIT TEMPLATE -->
    <ng-template #initialTemplate>Init Save</ng-template>

    <ui-loading-button
        [initialTemplate]="initialTemplate"
    >
    </ui-loading-button>
```

### example usage 2 with composision template
```html
    <ui-loading-button>
        <ng-template #buttonContent>
            Save
        </ng-template>
    </ui-loading-button>
```