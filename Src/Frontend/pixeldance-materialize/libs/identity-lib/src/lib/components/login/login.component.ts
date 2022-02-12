import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { IdentityService } from '../../services/identity.service';
import { IdentityConfig } from '../../identity-config';
import { BaseIdentityDialogComponent } from '../base-identity-dialog/base-identity-dialog.component';

@Component({
	selector: 'auth-login',
	templateUrl: './login.component.html',
	styleUrls: ['../base-identity-dialog/base-identity-dialog.component.scss'],
})
export class LoginComponent extends BaseIdentityDialogComponent implements OnInit {
	//
	constructor(protected config: IdentityConfig, protected router: Router, protected identityService: IdentityService) {
		super(config, router, identityService);
		this.routingUrl = this.config.unauhtorizedRoute;
	}
}
