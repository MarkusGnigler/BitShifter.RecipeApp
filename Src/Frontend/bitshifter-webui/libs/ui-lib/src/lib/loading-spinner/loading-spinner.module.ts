import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { MaterializeModule } from '../plugins/materialize.module';
import { LoadingSpinnerComponent } from './loading-spinner.component';
import { LoadingSpinnerInterceptor } from './loading-spinner.interceptor';

@NgModule({
	declarations: [LoadingSpinnerComponent],
	exports: [LoadingSpinnerComponent],
	imports: [CommonModule, MaterializeModule],
	providers: [
		//
		{
			provide: HTTP_INTERCEPTORS,
			useClass: LoadingSpinnerInterceptor,
			multi: true,
		},
	],
})
export class LoadingSpinnerModule {}
