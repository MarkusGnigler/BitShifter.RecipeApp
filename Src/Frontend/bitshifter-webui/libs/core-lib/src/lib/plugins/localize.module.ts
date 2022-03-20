import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';

import { DateAdapter, MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

import localeDe from '@angular/common/locales/de';
import localeDeExtra from '@angular/common/locales/extra/de';

import { bsDateFormatProviders } from './bs-date-adapter';

const LOCALIZATION = 'de-DE';
registerLocaleData(localeDe, LOCALIZATION, localeDeExtra);

// import * as moment from 'moment';
// moment.locale(LOCALIZATION);

@NgModule({
	providers: [...bsDateFormatProviders, { provide: MAT_DATE_LOCALE, useValue: LOCALIZATION }],
	imports: [CommonModule, MatDatepickerModule, MatNativeDateModule],
})
export class LocalizeModule {
	//
	constructor(private dateAdapter: DateAdapter<Date>) {
		this.dateAdapter.setLocale(LOCALIZATION);
	}
}
