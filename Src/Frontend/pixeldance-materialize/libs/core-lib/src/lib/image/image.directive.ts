import { Directive, HostBinding, Input } from '@angular/core';
import { CoreLibConfig } from '../core-lib.config';

@Directive({
	// eslint-disable-next-line  @angular-eslint/directive-selector
	selector: 'img[default]',
	// eslint-disable-next-line @angular-eslint/no-host-metadata-property
	host: {
		'(error)': 'updateUrl()',
		'(load)': 'load()',
		'[src]': 'src',
	},
})
export class ImageDirective {
	//
	@Input()
	src!: string;
	@Input() default!: string;

	@HostBinding('class') className!: unknown;

	constructor(private config: CoreLibConfig) {}

	// 	constructor({ nativeElement }: ElementRef<HTMLImageElement>, config: CoreLibConfig) {
	//     nativeElement.src = `${config.apiUrl}/${nativeElement.src}`;
	// 	}

	updateUrl() {
		this.src = `${this.config?.apiUrl}file/${this.src.replace('/', '%2F')}`;
		// this.src = this.default;
	}

	load() {
		this.className = 'image-loaded';
	}
}
