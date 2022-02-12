// Object overrides

// eslint-disable-next-line  @typescript-eslint/no-explicit-any
const isObjectEmpty = function (obj: any) {
	if (obj === undefined) return 0;
	return Object.keys(obj).length === 0;
};

export { isObjectEmpty };

/* 
	### In app.main.ts
	* import './prototypes.ts';
*/
