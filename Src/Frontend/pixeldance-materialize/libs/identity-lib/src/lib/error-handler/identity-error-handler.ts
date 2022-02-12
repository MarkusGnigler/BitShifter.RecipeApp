import { NEVER, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IdentityService } from '../services/identity.service';

export const handlerExternUserError =
	(identityService: IdentityService, resetLocalStorage: boolean) =>
	<T>(source: Observable<T>) =>
		source.pipe(
			catchError(
				// eslint-disable-next-line @typescript-eslint/no-explicit-any
				(error: any): Observable<T> => {
					// console.log('Identity', error);
					if (error.status == 401) {
						identityService.setExternUserError(resetLocalStorage);
					}
					return NEVER;
				},
			),
		);
