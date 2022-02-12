
# Example usage
```html
<ui-chips-picker
    title="Mitarbeiter"
    [selectable]="false"
    [removable]="true"
    [possibleOptions]="possibleOptions"
    [options]="options"
    (addOption)="onAddOption($event)"
    (removeOption)="onRemoveOption($event)"
></ui-chips-picker>
```