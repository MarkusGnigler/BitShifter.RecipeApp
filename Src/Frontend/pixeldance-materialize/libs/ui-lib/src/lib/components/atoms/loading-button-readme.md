
### Example usage 1 with initial template
```html
    <!-- INIT TEMPLATE -->
    <ng-template #initialTemplate>Init Save</ng-template>

    <pxd-loading-button
        [initialTemplate]="initialTemplate"
    >
    </pxd-loading-button>
```

### example usage 2 with composision template
```html
    <pxd-loading-button>
        <ng-template #buttonContent>
            Save
        </ng-template>
    </pxd-loading-button>
```