
## Example usage
```html
    <core-form-array-selector
        [columNames]="['Titel', 'Menge', 'Einheit']"
        [group]="group"
        arrayName="ingredients"
        [formArray]="ingredients"
    >
        <ng-template #arrayTemplate let-item let-id="index">
            <td>
                <mat-form-field appearance="fill">
                    <mat-label>Titel:</mat-label>
                    <input matInput required formControlName="title" />
                </mat-form-field>
            </td>
            <td>
                <mat-form-field appearance="fill">
                    <mat-label>Menge:</mat-label>
                    <input matInput type="number" required formControlName="quantity" />
                </mat-form-field>
            </td>
            <td>
                <mat-form-field appearance="fill">
                    <mat-label>Einheit:</mat-label>
                    <input matInput required formControlName="unit" />
                </mat-form-field>
            </td>
        </ng-template>
    </core-form-array-selector>
```