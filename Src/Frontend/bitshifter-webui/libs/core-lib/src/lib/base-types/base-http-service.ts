import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CoreLibConfig } from '../core-lib.config';

export abstract class BaseHttpService {
	//
	protected featureUrl = ``;
	protected readonly BASE_URL = `${this.config?.apiUrl}/${this.featureUrl}`;

	constructor(protected config: CoreLibConfig, protected http: HttpClient) {}

	protected get<T>(options = {}): Observable<T> {
		return this.http.get<T>(this.BASE_URL, options);
	}

	protected post<T>(body: unknown): Observable<T> {
		return this.http.post<T>(this.BASE_URL, body);
	}

	protected put<T>(body: unknown): Observable<T> {
		return this.http.put<T>(this.BASE_URL, body);
	}

	// eslint-disable-next-line @typescript-eslint/no-unused-vars
	protected delete<T>(body: unknown): Observable<T> {
		// TODO
		return this.http.delete<T>(this.BASE_URL);
	}

	protected patch<T>(body: unknown): Observable<T> {
		return this.http.patch<T>(this.BASE_URL, body);
	}
}
