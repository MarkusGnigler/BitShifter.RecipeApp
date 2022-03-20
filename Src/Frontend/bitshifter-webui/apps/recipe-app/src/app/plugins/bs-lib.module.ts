import { NgModule } from '@angular/core';
import { CoreLibModule } from '@bitshifter-webui/core-lib';
import { IdentityLibModule } from '@bitshifter-webui/identity-lib';
import { UiLibModule } from '@bitshifter-webui/ui-lib';

const baseLibs = [CoreLibModule, UiLibModule, IdentityLibModule];

@NgModule({
	imports: [baseLibs],
	exports: [baseLibs],
})
export class BsLibModule { }
