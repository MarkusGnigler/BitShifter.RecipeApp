import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from './busy.service';

@Injectable()
export class LoadingSpinnerInterceptor implements HttpInterceptor {
	//
	constructor(private busyService: BusyService) {}

	intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
		// console.log(DISABLE_HEADER, request.headers.get(DISABLE_HEADER));

		this.busyService.busy();
    

		return next.handle(request).pipe(
			delay(500),
			finalize(() => {
				this.busyService.idle();
			}),
		);
	}
}
