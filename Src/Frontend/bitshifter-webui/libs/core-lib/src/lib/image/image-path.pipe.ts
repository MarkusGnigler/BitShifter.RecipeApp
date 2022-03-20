import { Pipe, PipeTransform } from '@angular/core';
import { CoreLibConfig } from '../core-lib.config';

@Pipe({
	name: 'imagePath',
})
export class ImagePathPipe implements PipeTransform {
	//
	constructor(private config: CoreLibConfig) {}

	transform(path: string): string {
		return `${this.config.apiUrl}file/${path.replace('/', '%2F')}`;
	}
}