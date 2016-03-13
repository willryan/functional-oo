
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
function lCompose(func1, func2) {
  return function() {
    return func1(func2.apply(undefined, arguments));
  };
}

function rCompose(func2, func1) {
  return lCompose(func1, func2);
}

describe('composition', () => {
  it('can compose', () => {
    const add2Mult2 = rCompose(add2, mult2);
    expect(add2Mult2(3)).toBe(10);
    const mult2Add2 = lCompose(add2, mult2);
    expect(mult2Add2(3)).toBe(8);
  });
});

describe('DI example', () => {
  xit('shows partial application through IOC', () => {
  });
});
