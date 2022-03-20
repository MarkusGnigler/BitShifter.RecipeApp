import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Role } from '../models/role';
import { User } from '../models/user';
import { IdentityService } from '../services/identity.service';

@Injectable({
	providedIn: 'root',
})
export class AdminGuard implements CanActivate {
	//
	constructor(private snackBar: MatSnackBar, private identityService: IdentityService) {}

	canActivate(): Observable<boolean> {
		//
		return this.identityService.currentUser$.pipe(
			map((user: User | null) => {
				if (user?.roles?.includes(Role.Admin)) return true;

				console.warn(`Message from auth.guard => Access not allowed for user "${user?.userName}"`);

				this.snackBar.open('Zugriff nicht erlaubt', 'Ok', {
					duration: 2500,
					horizontalPosition: 'center',
					verticalPosition: 'top',
				});

				return false;
			}),
		);
	}
}
