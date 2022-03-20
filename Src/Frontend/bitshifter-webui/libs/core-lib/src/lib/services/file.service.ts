import { HttpClient, HttpEventType, HttpProgressEvent } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, iif, Observable, of } from 'rxjs';
import { filter, mergeMap, tap } from 'rxjs/operators';
import { CoreLibConfig } from '../core-lib.config';

@Injectable({
	providedIn: 'root',
})
export class FileService {
	//
	protected featureUrl = 'file';

	private readonly _downloadProgress = new BehaviorSubject<number>(0);
	readonly downloadProgress$ = this._downloadProgress.asObservable();

	onDownloadFinished = new EventEmitter<File>();

	private readonly _uploadProgress = new BehaviorSubject<number>(0);
	readonly uploadProgress$ = this._uploadProgress.asObservable();

	onUploadFinished = new EventEmitter();

	constructor(private config: CoreLibConfig, private http: HttpClient) {}

	downloadFile(filename: string) {
		if (filename === '') return EMPTY;

		return this.http
			.get(`${this.config.apiUrl}file/${filename}`, {
				reportProgress: true,
				observe: 'events',
				responseType: 'blob',
			})
			.pipe(
				// eslint-disable-next-line  @typescript-eslint/no-explicit-any
				mergeMap((event: any) =>
					iif(
						() => event.type === HttpEventType.Response,
						this.downloadFile$(filename)(event),
						this.progressDownload$(event),
					),
				),
				//
				// tap((event: any) => {
				// 	if (event.type === HttpEventType.DownloadProgress && event.total !== undefined) {
				// 		this._downloadProgress.next(this.calculateProgress(event));
				// 	}
				// }),
				// filter((event: any) => event.type === HttpEventType.Response),
				// map((event: any) => new File([event.body], filename)),
				// tap((file: File) => this.onDownloadFinished.emit(file)),
			);
	}

	private downloadFile$ =
		(filename: string) =>
		// eslint-disable-next-line  @typescript-eslint/no-explicit-any
		(event: any): Observable<File> =>
			of(new File([event.body], filename)).pipe(tap((file: File) => this.onDownloadFinished.emit(file)));

	// eslint-disable-next-line  @typescript-eslint/no-explicit-any
	private progressDownload$ = (event: any): Observable<any> =>
		of(event).pipe(
			filter(e => e.type === HttpEventType.DownloadProgress && e.total !== undefined),
			tap(e => this._downloadProgress.next(this.calculateProgress(e))),
		);

	uploadFile(file: File) {
		if (!file) return;

		const formData = new FormData();
		formData.append('file', file);

		this.http
			.post(`${this.config.apiUrl}file/`, formData, {
				reportProgress: true,
				observe: 'events',
			})
			.subscribe(event => {
				if (event.type === HttpEventType.UploadProgress && event.total !== undefined) {
					this._uploadProgress.next(this.calculateProgress(event));
				} else if (event.type === HttpEventType.Response) {
					this._uploadProgress.next(100);
					this.onUploadFinished.emit(event.body);
				}
			});
	}

	private calculateProgress(event: HttpProgressEvent): number {
		if (event.total == undefined) return 0;
		return Math.round((100 * event.loaded) / event.total);
	}
}
