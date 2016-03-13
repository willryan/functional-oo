
const add2 = function(x) { return x + 2 };

const mult = function(x, y) { return x * y };

// bind, apply, etc.
const mult2 = mult.bind(undefined, 2);

describe('bind', () => {
  it('can do partial application', () => {
    expect(mult2(5)).toBe(10);
  });
});

// composition
describe('composition', () => {
  xit('can compose', () => {
    //const add2Mult2 = compose(add2, mult);
  });
});

