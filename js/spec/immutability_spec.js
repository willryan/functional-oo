const Immutable = require('immutable');

'use strict';

// use const for immutable in scope, but only good for variables

describe('immutability', () => {
  it ('creates copies on changes', () => {
    const map1 = Immutable.Map({a: 1, b: 2});
    const map2 = map1.set('b', 50);
    expect(map1.get('b')).toBe(2);
    expect(map2.get('b')).toBe(50);
  });
});

describe('freeze()', () => {
  it('hopefully stops changes', () => {
    const stuff = { c: 3, d: 4 };
    const frozen = Object.freeze(stuff);
    expect(() => frozen.c = 4).toThrow();
    expect(() => frozen.e = 10).toThrow();
    expect(frozen.c).toBe(3);
  });
  it('does not do a deep freeze', () => {
    const stuff = { f: 5, g: { h: "6" } };
    const frozen = Object.freeze(stuff);
    expect(() => frozen.f = 4).toThrow();
    expect(() => frozen.g.h = 10).not.toThrow();
    expect(frozen.g.h).toBe(10);
  });
});

