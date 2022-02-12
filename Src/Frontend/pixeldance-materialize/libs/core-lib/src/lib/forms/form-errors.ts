import { InjectionToken } from '@angular/core';

interface LengthParameter {
	requiredLength: number;
	actualLength: number;
}

export const defaultErrors = {
	// eslint-disable-next-line  @typescript-eslint/no-unused-vars
	required: (error: unknown) => `This field is required`,
	minlength: (length: LengthParameter) => `Expect ${length.requiredLength} but got ${length.actualLength}`,
	// minlength: (requiredLength: number, actualLength: number) => `Expect ${requiredLength} but got ${actualLength}`
};

export const FORM_ERRORS = new InjectionToken('FORM_ERRORS', {
	providedIn: 'root',
	factory: () => defaultErrors,
});
