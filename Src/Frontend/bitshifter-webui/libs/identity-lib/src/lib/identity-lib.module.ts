import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdentityConfig } from './identity-config';

import { CoreLibModule } from '@bitshifter-webui/core-lib';
import { UiLibModule } from '@bitshifter-webui/ui-lib';

import { BaseIdentityDialogComponent } from './components/base-identity-dialog/base-identity-dialog.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

const identity = [BaseIdentityDialogComponent, LoginComponent, RegisterComponent];

@NgModule({
	imports: [CommonModule, UiLibModule, CoreLibModule],
	declarations: [identity],
	exports: [identity],
})
export class IdentityLibModule {
	//
	public static forRoot(configuration: IdentityConfig): ModuleWithProviders<IdentityLibModule> {
		return {
			ngModule: IdentityLibModule,
			providers: [
				{
					provide: IdentityConfig,
					useValue: configuration,
				},
			],
		};
	}
}
