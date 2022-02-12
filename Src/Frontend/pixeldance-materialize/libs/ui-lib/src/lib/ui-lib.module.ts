import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterializeModule } from './plugins/materialize.module';
import { LayoutModule } from './layout/layout.module';
import { ComponentsModule } from './components/components.module';
import { LoadingSpinnerModule } from './loading-spinner/loading-spinner.module';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { ErrorPagesModule } from './error-pages/error-pages.module';

@NgModule({
	imports: [CommonModule, LoadingSpinnerModule],
	exports: [
		LayoutModule,
		CoreLibModule,
		FlexLayoutModule,
		MaterializeModule,
		ErrorPagesModule,
		ComponentsModule,
		LoadingSpinnerModule,
	],
})
export class UiLibModule {}
