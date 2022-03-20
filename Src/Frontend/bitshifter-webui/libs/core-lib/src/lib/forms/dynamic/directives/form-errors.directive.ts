import {
	ComponentFactoryResolver,
	ComponentRef,
	Directive,
	Host,
	Inject,
	Input,
	OnDestroy,
	OnInit,
	Optional,
	ViewContainerRef,
} from '@angular/core';
import { EMPTY, merge, Observable, of, Subscription } from 'rxjs';
import { FormErrorComponent } from '../form-error.component';
import { FormSubmitDirective } from './form-submit.directive';

import { ControlContainer, NgControl, ValidationErrors } from '@angular/forms';
import { FORM_ERRORS } from '../../form-errors';

/*
https://netbasal.com/make-your-angular-forms-error-messages-magically-appear-1e32350b7fa5
*/

@Directive({
	// selector: '[formControl], [formControlName]'
	selector: '[coreHasError]',
})
export class FormErrorsDirective implements OnInit, OnDestroy {
	// @Input() coreHasError!: ViewContainerRef
	@Input() errorContainerRef!: ViewContainerRef;

	private subscription$!: Subscription;
	private submit$: Observable<Event>;
	private ref!: ComponentRef<FormErrorComponent>;

	constructor(
		private viewContainerRef: ViewContainerRef,
		private resolver: ComponentFactoryResolver,

		// eslint-disable-next-line  @typescript-eslint/no-explicit-any
		@Inject(FORM_ERRORS) private errors: any,

		@Optional() private controlDir: NgControl,
		@Optional() @Host() private form: FormSubmitDirective,
		@Optional() private controlContainer: ControlContainer,
	) {
		this.submit$ = this.form ? this.form.submit$ : EMPTY;
		console.log('form', this.form);
		console.log('controlDir', this.controlDir);
		console.log('controlContainer', this.controlContainer.control);
	}

	get control() {
		return this.controlDir ? this.controlDir : this.controlContainer.control;
	}

	ngOnInit() {
		console.log('errorContainerRef', this.errorContainerRef);

		this.subscription$ = merge([
			this.submit$,
			// this.control.statusChanges,
			this.control?.valueChanges,
			of(this.control?.touched === true),
		])
			// .pipe(
			//   untilDestroyed(this)
			// )
			.subscribe(() => {
				console.log('event', this.control?.errors);
				const controlErrors = this.control?.errors;

				if (controlErrors) {
					this.setError(this.getErrorMessage(controlErrors));
				} else if (this.ref) {
					this.setError(null);
				}
			});
	}

	ngOnDestroy(): void {
		this.subscription$.unsubscribe();
	}

	private getErrorMessage(controlErrors: ValidationErrors) {
		const errorType = Object.keys(controlErrors)[0];
		const errorValue = controlErrors[errorType];
		const getError = this.errors[errorType];
		return getError(errorValue);
	}

	private setError(text: string | null) {
		this.resolveErrorComponent();
		this.ref.instance.text = `${text}`;
	}

	private resolveErrorComponent() {
		if (this.ref) return;
		// console.log('this.coreHasError', this.coreHasError)
		console.log('this.viewContainerRef', this.viewContainerRef);

		this.ref = this.viewContainerRef.createComponent(this.resolver.resolveComponentFactory(FormErrorComponent));
	}
}
