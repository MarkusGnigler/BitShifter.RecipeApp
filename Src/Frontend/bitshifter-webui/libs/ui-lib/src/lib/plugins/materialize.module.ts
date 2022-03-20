import { NgModule } from '@angular/core';

import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatSliderModule } from '@angular/material/slider';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatTooltipModule} from '@angular/material/tooltip';

const materialModules = [
	MatFormFieldModule,
	MatDialogModule,
	MatButtonModule,
	MatIconModule,
	MatInputModule,
	MatTableModule,
	MatPaginatorModule,
	MatOptionModule,
	MatProgressSpinnerModule,
	MatCardModule,
	MatProgressBarModule,
	MatSidenavModule,
	MatListModule,
	MatToolbarModule,
	MatMenuModule,
	MatDatepickerModule,
	MatNativeDateModule,
	MatSnackBarModule,
	MatSelectModule,
	MatCheckboxModule,
	MatTabsModule,
	MatRadioModule,
	MatButtonToggleModule,
	MatExpansionModule,
	MatChipsModule,
	MatAutocompleteModule,
	MatSliderModule,
	MatRippleModule,
	MatStepperModule,
	MatSlideToggleModule,
	MatTooltipModule
];

@NgModule({
	imports: materialModules,
	exports: materialModules,
})
export class MaterializeModule {}
