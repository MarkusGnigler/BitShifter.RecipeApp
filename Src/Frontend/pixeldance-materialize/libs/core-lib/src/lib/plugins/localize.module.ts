import { NgModule } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';

import { DateAdapter, MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

import localeDe from '@angular/common/locales/de';
import localeDeExtra from '@angular/common/locales/extra/de';

import { pxdDateFormatProviders } from './pxd-date-adapter';

import * as moment from 'moment';
const LOCALIZATION = 'de-DE';
registerLocaleData(localeDe, LOCALIZATION, localeDeExtra);
moment.locale(LOCALIZATION);

@NgModule({
	providers: [...pxdDateFormatProviders, { provide: MAT_DATE_LOCALE, useValue: LOCALIZATION }],
	imports: [CommonModule, MatDatepickerModule, MatNativeDateModule],
})
export class LocalizeModule {
	//
	constructor(private dateAdapter: DateAdapter<Date>) {
		this.dateAdapter.setLocale(LOCALIZATION);
	}
}
