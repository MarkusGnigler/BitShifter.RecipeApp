import { NgModule } from '@angular/core';
import { AsyncImageDirective } from './async-image.directive';
import { ImagePathPipe } from './image-path.pipe';
import { LazyImageDirective } from './lazy-image.directive';

const pipesAndDirectives = [ImagePathPipe, AsyncImageDirective, LazyImageDirective];

@NgModule({
	exports: [pipesAndDirectives],
	declarations: [pipesAndDirectives],
})
export class ImageModule {}
