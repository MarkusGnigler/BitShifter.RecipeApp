import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { IdentityService } from '../services/identity.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
	//
	constructor(private identityService: IdentityService) {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		//
		this.identityService.currentUser$.pipe(take(1)).subscribe(user => {
			if (!user) return next.handle(request);

			request = request.clone({
				setHeaders: {
					Authorization: `Bearer ${user.token}`,
				},
			});

			return next.handle(request);
		});

		return next.handle(request);
	}
}
