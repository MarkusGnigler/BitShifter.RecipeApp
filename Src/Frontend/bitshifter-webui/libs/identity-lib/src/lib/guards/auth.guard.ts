import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { IdentityConfig } from '../identity-config';
import { User } from '../models/user';
import { IdentityService } from '../services/identity.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard implements CanActivate {
	//
	constructor(private config: IdentityConfig, private router: Router, private identityService: IdentityService) {}

	canActivate(route: ActivatedRouteSnapshot): Observable<boolean> {
		if (route.url[0].path == this.config.loginRoute) return of(false);

		return this.identityService.currentUser$.pipe(
			map((user: User | null) => {
				if (user && !user?.blockGuard) return true;

				this.router.navigate([this.config.loginRoute]);

				return false;
			}),
		);
	}
}
