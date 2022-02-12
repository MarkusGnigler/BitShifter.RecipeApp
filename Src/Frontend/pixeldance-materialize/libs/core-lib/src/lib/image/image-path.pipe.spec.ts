import { CoreLibConfig } from '../core-lib.config';
import { ImagePathPipe } from './image-path.pipe';

describe('TestPipe', () => {
	const config: CoreLibConfig = {
		apiUrl: 'API/',
		isProduction: false,
	};

	let pipe: ImagePathPipe | undefined = undefined;

	beforeEach(() => {
		pipe = new ImagePathPipe(config);
	});

	it('create an instance', () => {
		expect(pipe).toBeTruthy();
	});

  it('parse image url', () => {
    expect(pipe?.transform('test-image-name')).toBe('API/file/test-image-name');
  });
});
