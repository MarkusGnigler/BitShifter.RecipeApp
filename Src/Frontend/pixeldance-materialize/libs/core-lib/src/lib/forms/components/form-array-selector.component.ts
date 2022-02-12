import { Component, ContentChild, EventEmitter, Input, Output, TemplateRef } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';

export const ITEM_TEMPLATE = 'arrayTemplate';

@Component({
	selector: 'pxd-form-array-selector',
	template: `
		<table [formGroup]="group">
			<thead>
				<tr>
					<th *ngFor="let header of columNames">{{header}}</th>
				</tr>
			</thead>

			<tbody>
				<ng-container [formArrayName]="arrayName">
					<ng-container *ngFor="let formArray of formArray.controls; let i = index">
						<tr>
							<ng-container [formGroupName]="i">
								<ng-template #defaultItem>
									{{ formArray | json }}
								</ng-template>

								<ng-container
									[ngTemplateOutlet]="arrayTemplateRef || defaultItem"
									[ngTemplateOutletContext]="{ $implicit: formArray, index: i }"
								></ng-container>

								<td>
									<button
										mat-mini-fab
										color="warn"
										type="button"
										style="margin-bottom: 1.34375em"
										(click)="removeFormArray.emit(i)"
									>
										<mat-icon>remove</mat-icon>
									</button>
								</td>
							</ng-container>
						</tr>
					</ng-container>
				</ng-container>
			</tbody>
		</table>
	`,
	styles: [
		`
			table {
				width: 100%;
			}
		`,
	],
})
export class FormArraySelectorComponent {
	//
	@Input() columNames!: string[];
	@Input() group!: FormGroup;
	@Input() arrayName!: string;
	@Input() formArray!: FormArray;

	@Output() removeFormArray = new EventEmitter<number>();

	@ContentChild(ITEM_TEMPLATE, { static: false })
	arrayTemplateRef!: TemplateRef<HTMLElement>;
}
