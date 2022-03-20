
### Standard example usage
```TS
    @Component({
        selector: 'dialog-content-example-dialog',
        template: `
            <!-- DIALOG BUTTONS -->
            <ng-template #dialogActions>
                <button mat-raised-button color="primary" (click)="onButtonClick()">Outside button</button>
                <button mat-raised-button color="warn" [mat-dialog-close]>Close</button>
            </ng-template>
            
            <!-- DIALOG -->
            <ui-base-dialog 
                title="Test Dialog"
                [disableClose]="false"
                [dialogActionsTemplate]="dialogActions"
            >

                <ng-container>
                    <p>
                        Content
                    </p>
                </ng-container>
                
            </ui-base-dialog>
        `
    })
    export class DialogContentExampleDialog {

        onButtonClick() {
            console.log('Button clicked');
        }

    }
```

### Forms example usage
    * To submit an external form, it is necessary that the form has a unique ID and the button has a form attribute with this ID.
```TS
    @Component({
        selector: 'dialog-content-example-dialog',
        template: `
            <!-- DIALOG BUTTONS -->
            <ng-template #dialogActions>
                <button mat-raised-button color="primary" [disabled]="formGroup.valid" form="ui-form">
                    Speichern
                </button>
                <button mat-raised-button color="warn" [mat-dialog-close]>Close</button>
            </ng-template>
            
            <!-- DIALOG -->
            <ui-base-dialog 
                title="Test Dialog"
                [disableClose]="false"
                [dialogActionsTemplate]="dialogActions"
            >

                <form #uiForm id="ui-form" [formGroup]="formGroup" (ngSubmit)="onSubmit()">
                    <mat-form-field>
                        <mat-label>Name:</mat-label>
                        <input matInput formControlName="name" autocomplete="off" />
                    </mat-form-field>
	            </form>
                
            </ui-base-dialog>
        `
    })
    export class DialogContentExampleDialog {

    }
```