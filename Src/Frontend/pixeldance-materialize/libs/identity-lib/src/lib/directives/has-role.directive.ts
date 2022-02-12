import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { User } from '../models/user';
import { IdentityService } from '../services/identity.service';

@Directive({
	// eslint-disable-next-line @angular-eslint/directive-selector
	selector: '[hasRole]',
})
export class HasRoleDirective implements OnInit {
	//
	@Input() hasRole!: string[];

	user!: User | null;

	constructor(
		private templateRef: TemplateRef<HTMLElement>,
		private viewContainerRef: ViewContainerRef,
		private identityService: IdentityService,
	) {
		this.identityService.currentUser$.pipe(take(1)).subscribe(user => {
			this.user = user ?? null;
		});
	}

	ngOnInit(): void {
		// clear view if no roles
		console.log('hasRole => user:', this.user);
		if (!this.user?.roles || this.user == null) {
			this.viewContainerRef.clear();
			return;
		}

		// eslint-disable-next-line @typescript-eslint/no-explicit-any
		if (this.user?.roles.some((r: any) => this.hasRole.includes(r))) {
			this.viewContainerRef.createEmbeddedView(this.templateRef);
		} else {
			this.viewContainerRef.clear();
		}
	}
}
