/**
 * Generates a random guid
 * @returns
 */
export const generateGuid = () => {
	let actualTime = new Date().getTime(),
		d2 = (performance && performance.now && performance.now() * 1000) || 0;
	return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
		let randomNumber = Math.random() * 16;
		if (actualTime > 0) {
			randomNumber = (actualTime + randomNumber) % 16 | 0;
			actualTime = Math.floor(actualTime / 16);
		} else {
			randomNumber = (d2 + randomNumber) % 16 | 0;
			d2 = Math.floor(d2 / 16);
		}
		return (c == 'x' ? randomNumber : (randomNumber & 0x7) | 0x8).toString(16);
	});
};

export const guidEmpty = () => '00000000-0000-0000-0000-000000000000';
