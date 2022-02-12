import { Injectable } from '@angular/core';
import { DateAdapter, MAT_DATE_FORMATS, NativeDateAdapter } from '@angular/material/core';

//https://stackblitz.com/edit/angular-dateadapter-example-stackoverflow1-u6zvfv?file=main.ts

/** Adapts the native JS Date for use with cdk-based components that work with dates. */
@Injectable()
export class PxdDateAdapter extends NativeDateAdapter {
	// parse the date from input component as it only expect dates in
	// mm-dd-yyyy format
	// parse(value: any): Date | null {
	// 	if (typeof value === 'string' && value.indexOf('/') > -1) {
	// 		const str = value.split('/');

	// 		const year = Number(str[2]);
	// 		const month = Number(str[1]) - 1;
	// 		const date = Number(str[0]);

	// 		return new Date(year, month, date);
	// 	}
	// 	const timestamp = typeof value === 'number' ? value : Date.parse(value);
	// 	return isNaN(timestamp) ? null : new Date(timestamp);
	// }

	// format(date: Date, displayFormat: Object): string {
	// 	if (displayFormat === 'input') {
	// 		const day = date.getUTCDate();
	// 		const month = date.getUTCMonth() + 1;
	// 		const year = date.getFullYear();
	// 		// Return the format as per your requirement
	// 		return `${year}-${month}-${day}`;
	// 	}
	// 	return date.toDateString();
	// }

	getFirstDayOfWeek(): number {
		return 1;
	}
}

// const DATEPICKER_FORMATS: NgxMatDateFormats = {
const DATEPICKER_FORMATS = {
	parse: {
		dateInput: 'DD.MM.YYYY HH:mm:ss',
	},
	display: {
		dateInput: 'DD.MM.YYYY HH:mm:ss',
		monthYearLabel: 'YYYY',
		dateA11yLabel: 'LL',
		monthYearA11yLabel: 'YYYY',
	},
};

const INTL_DATE_INPUT_FORMAT = {
	year: 'numeric',
	month: 'numeric',
	day: 'numeric',
	hourCycle: 'h23',
	hour: '2-digit',
	minute: '2-digit',
};

//   const CUSTOM_MAT_DATE_FORMATS: NgxMatDateFormats = {
const CUSTOM_MAT_DATE_FORMATS = {
	parse: {
		dateInput: INTL_DATE_INPUT_FORMAT,
	},
	display: {
		dateInput: INTL_DATE_INPUT_FORMAT,
		monthYearLabel: { year: 'numeric', month: 'short' },
		dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
		monthYearA11yLabel: { year: 'numeric', month: 'long' },
	},
};

export const pxdDateFormatProviders = [
	{ provide: MAT_DATE_FORMATS, useValue: DATEPICKER_FORMATS },
	{ provide: MAT_DATE_FORMATS, useValue: CUSTOM_MAT_DATE_FORMATS },
	{ provide: DateAdapter, useClass: PxdDateAdapter },
    // {provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE]},
    // {provide: MAT_DATE_FORMATS, useValue: MAT_MOMENT_DATE_FORMATS},
];
