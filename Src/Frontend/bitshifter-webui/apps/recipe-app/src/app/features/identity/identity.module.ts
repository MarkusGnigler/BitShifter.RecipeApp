import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IdentityRoutingModule } from './identity.routing';
import { LoginComponent } from './login.component';
import { BsLibModule } from '../../plugins/bs-lib.module';
import { RegisterComponent } from './register.component';

@NgModule({
	declarations: [LoginComponent, RegisterComponent],
	imports: [CommonModule, IdentityRoutingModule, BsLibModule],
})
export class IdentityModule {}
