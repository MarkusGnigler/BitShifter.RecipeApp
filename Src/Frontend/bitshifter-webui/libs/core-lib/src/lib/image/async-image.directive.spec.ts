import { Component, ViewChild } from '@angular/core';
import { AsyncImageDirective } from './async-image.directive';
import { TestBed, ComponentFixture } from '@angular/core/testing';
import { By } from '@angular/platform-browser';

// Simple test component that will not in the actual app
@Component({
	template: `
		<!-- <img [src]="'testpath' | imagePath" /> -->
		<img id="img" #img *coreAsyncImage src="testpath" />
	`,
})
class TestImageComponent {
	@ViewChild('img') image: HTMLElement | undefined = undefined;
}

describe('AsyncImageDirective', () => {
	let component: TestImageComponent;
	let fixture: ComponentFixture<TestImageComponent>;

	beforeEach(async () => {
		await TestBed.configureTestingModule({
			declarations: [TestImageComponent],
		}).compileComponents();
	});

	beforeEach(() => {
		fixture = TestBed.createComponent(TestImageComponent);
		component = fixture.componentInstance;
		fixture.detectChanges();
	});

	it('should create an instance', () => {
		expect(component).toBeTruthy();
	});

	it('directive should add attribute', () => {
		const debugEl: HTMLElement = fixture.debugElement.nativeElement;
		const image: HTMLElement | null = debugEl.querySelector('img');
		const image2 = fixture.debugElement.query(By.css('#img'));
		const element = fixture.debugElement.nativeElement.querySelector('img');

		// const bannerDe: DebugElement = fixture.debugElement;
		const paragraphDe = fixture.debugElement.query(By.css('img'));
		// const p: HTMLElement = paragraphDe.nativeElement;

		// console.log(debugEl);
		console.log(paragraphDe);
		console.log(element);
		console.log(image);
		console.log(image2);
		console.log(image?.attributes);

		// expect(image?.attributes).toContain('async');
		expect(element.decoding).toContain('async');
		// expect(fixture.nativeElement.getAttribute('disabled')).toEqual('async');
	});
});
