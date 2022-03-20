// enum YesNo {
//     No = 0,
//     Yes = 1,
// }

// console.log(YesNo[YesNo.Yes]);
// console.log(YesNo[YesNo.No]);
// console.log(YesNo["Yes"]);
// console.log(YesNo["No"]);
// console.log(YesNo[1]);
// console.log(YesNo[0]);
// This code compiles into:

// var YesNo;
// (function (YesNo) {
// YesNo[YesNo["No"] = 0] = "No";
// YesNo[YesNo["Yes"] = 1] = "Yes";
// })(YesNo || (YesNo = {}));
// console.log(YesNo[YesNo.Yes]);
// console.log(YesNo[YesNo.No]);
// console.log(YesNo["Yes"]);
// console.log(YesNo["No"]);
// console.log(YesNo[1]);
// console.log(YesNo[0]);

export const getEnumNames = <TEnum>(value: TEnum) => {
	return (
		Object.keys(value)
			.filter(isStateEnum)
			// eslint-disable-next-line  @typescript-eslint/no-explicit-any
			.map(x => (value as any)[x])
	);
};

export const getEnumValues = <TEnum>(value: TEnum) => {
	return Object.values(value)
		.filter(isStateEnum)
		.map(x => x);
};

export const getEnumViewValues = <TEnum>(value: TEnum) => {
	const names = getEnumNames(value);
	const values = getEnumValues(value);
	return zip(values, names).map(x => ({ value: x[0], viewValue: x[1] }));
};

const isStateEnum = <TEnum>(value: TEnum) => !isNaN(Number(value));

/**
 * Apply a function to pairs of elements at the same index in two arrays, collecting the results in a new array. If one
 * input array is short, excess elements of the longer array are discarded.
 *
 * @example
 * import { zipWith } from 'fp-ts/Array'
 *
 * assert.deepStrictEqual(zipWith([1, 2, 3], ['a', 'b', 'c', 'd'], (n, s) => s + n), ['a1', 'b2', 'c3'])
 *
 * @category combinators
 * @since 2.0.0
 */
export const zipWith = <A, B, C>(fa: Array<A>, fb: Array<B>, f: (a: A, b: B) => C): Array<C> => {
	const fc: Array<C> = [];
	const len = Math.min(fa.length, fb.length);
	for (let i = 0; i < len; i++) {
		fc[i] = f(fa[i], fb[i]);
	}
	return fc;
};

/**
 * Takes two arrays and returns an array of corresponding pairs. If one input array is short, excess elements of the
 * longer array are discarded
 *
 * @example
 * import { zip } from 'fp-ts/Array'
 * import { pipe } from 'fp-ts/function'
 *
 * assert.deepStrictEqual(pipe([1, 2, 3], zip(['a', 'b', 'c', 'd'])), [[1, 'a'], [2, 'b'], [3, 'c']])
 *
 * @category combinators
 * @since 2.0.0
 */
export function zip<B>(bs: Array<B>): <A>(as: Array<A>) => Array<[A, B]>;
export function zip<A, B>(as: Array<A>, bs: Array<B>): Array<[A, B]>;
export function zip<A, B>(as: Array<A>, bs?: Array<B>): Array<[A, B]> | ((bs: Array<B>) => Array<[B, A]>) {
	if (bs === undefined) {
		return bs => zip(bs, as);
	}
	return zipWith(as, bs, (a, b) => [a, b]);
}
