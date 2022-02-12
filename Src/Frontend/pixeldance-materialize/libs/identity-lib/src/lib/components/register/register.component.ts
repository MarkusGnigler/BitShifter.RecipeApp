import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { IdentityService } from '../../services/identity.service';
import { IdentityConfig } from '../../identity-config';
import { BaseIdentityDialogComponent } from '../base-identity-dialog/base-identity-dialog.component';
import { FormControl, Validators } from '@angular/forms';

@Component({
	selector: 'auth-register',
	templateUrl: './register.component.html',
	styleUrls: ['../base-identity-dialog/base-identity-dialog.component.scss'],
})
export class RegisterComponent extends BaseIdentityDialogComponent implements OnInit {
	//
	showRepetitivePassword = false;

	constructor(protected config: IdentityConfig, protected router: Router, protected identityService: IdentityService) {
		super(config, router, identityService);
		this.routingUrl = this.config.unauhtorizedRoute;
	}

	ngOnInit(): void {
		super.ngOnInit();
		this.formGroup.addControl('passwordRepeated', new FormControl('', [Validators.required]));
	}

	async onSubmit() {
		console.error(`Remeber Me formGroup => ${this.getField('rememberMe')?.value}`);
		//TODO: auslagern in validator
		if (this.getField('password')?.value !== this.getField('passwordRepeated')?.value) return;

		// eslint-disable-next-line @typescript-eslint/no-unused-vars
		this.identityService.register(this.buildUser()).subscribe({ next: _ => this.router.navigate([this.routingUrl]) });
	}
}
