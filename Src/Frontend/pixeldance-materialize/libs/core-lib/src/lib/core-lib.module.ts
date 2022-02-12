import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CoreLibConfig } from './core-lib.config';
import { PxdFormsModule } from './forms/pxd-forms.module';
import { CdkModule } from './plugins/cdk.module';
import { LocalizeModule } from './plugins/localize.module';
import { SanitizeHtmlPipe } from './pipes/sanitize-html.pipe';
import { ImageModule } from './image/image.module';

const modules = [
	ImageModule, 
	LocalizeModule, PxdFormsModule, CdkModule];

const pipesAndDirectives = [SanitizeHtmlPipe];

@NgModule({
	imports: [CommonModule],
	exports: [modules, pipesAndDirectives],
	declarations: [pipesAndDirectives],
})
export class CoreLibModule {
	// Setup
	static forRoot(configuration: CoreLibConfig): ModuleWithProviders<CoreLibModule> {
		return {
			ngModule: CoreLibModule,
			providers: [{ provide: CoreLibConfig, useValue: configuration }],
		};
	}
}
