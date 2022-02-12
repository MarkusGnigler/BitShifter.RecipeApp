import { NgModule } from '@angular/core';
import { CoreLibModule } from '@pixeldance-materialize/core-lib';
import { IdentityLibModule } from '@pixeldance-materialize/identity-lib';
import { UiLibModule } from '@pixeldance-materialize/ui-lib';

const baseLibs = [CoreLibModule, UiLibModule, IdentityLibModule];

@NgModule({
	imports: [baseLibs],
	exports: [baseLibs],
})
export class PxdLibModule { }
