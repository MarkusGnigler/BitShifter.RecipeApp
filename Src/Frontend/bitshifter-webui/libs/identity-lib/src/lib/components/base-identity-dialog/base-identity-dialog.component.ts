import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseFormComponent, guidEmpty } from '@bitshifter-webui/core-lib';
import { IdentityConfig } from '../../identity-config';
import { User } from '../../models/user';
import { IdentityService } from '../../services/identity.service';

@Component({
	selector: 'auth-base-identity-dialog',
	template: '',
	styleUrls: ['./base-identity-dialog.component.scss'],
})
export class BaseIdentityDialogComponent extends BaseFormComponent {
	//
	loading = false;
	showPassword = false;
	routingUrl = this.config.loginRoute;

	get names() {
		return this.getArray('names');
	}

	constructor(protected config: IdentityConfig, protected router: Router, protected identityService: IdentityService) {
		super();
	}

	// eslint-disable-next-line @angular-eslint/use-lifecycle-interface
	override ngOnInit() {
		super.ngOnInit();
		if (this.identityService.getStateSnapshot().currentUser) {
			this.router.navigate([this.routingUrl]);
		}
	}

	protected override buildFormGroup(): FormGroup {
		return new FormGroup({
			userName: new FormControl('', [Validators.required]),
			password: new FormControl('', [Validators.required, Validators.minLength(8)]),
			rememberMe: new FormControl(true),
		});
	}

	onSubmit() {
		console.log(`this.routingUrl => ${this.routingUrl}`);
		console.log(`Remeber Me formGroup => ${this.getField('rememberMe')?.value}`);
		// eslint-disable-next-line @typescript-eslint/no-unused-vars
		this.identityService.login(this.buildUser()).subscribe({ next: _ => this.router.navigate([this.routingUrl]) });
	}

	protected buildUser(): User {
		return {
			id: guidEmpty(),
			userName: this.getField('userName')?.value,
			password: this.getField('password')?.value,
			rememberMe: this.getField('rememberMe')?.value,
		};
	}
}
