//  yield abuse
require('babel-polyfill');

const m = require('monet');
const Maybe = m.Maybe;

const factor = function (num, mod) {
  if (num % mod == 0) {
    return Maybe.Just(num / mod);
  } else {
    return Maybe.Nothing();
  }
};
const factors = function (num) {
  return new Maybe.Just(num).bind(x =>
    factor(x, 2).bind(y =>
      factor(y, 3).bind(z =>
        factor(z, 5))));
}

function doM(gen) {
  function step(value) {
    var result = gen.next(value);
    if (result.done) {
      return result.value;
    }
    return result.value.bind(step);
  }
  return step();
}

const factorsM = function (num) {
  return doM(function*() {
    var x = yield Maybe.Just(num);
    var y = yield factor(x, 2);
    var z = yield factor(y, 3);
    return factor(z, 5);
  }());
}
describe('monet', () => {
  it('is another way to do monads', () => {
    expect(factors(1).isNothing()).toBe(true);
    expect(factors(4).isNothing()).toBe(true);
    expect(factors(15).isNothing()).toBe(true);
    expect(factors(30).just()).toBe(1);
    expect(factors(420).just()).toBe(14);
  });

  it('can use syntactic sugar', () => {
    expect(factorsM(420).just()).toBe(14);
    expect(factorsM(30).just()).toBe(1);
    expect(factorsM(1).isNothing()).toBe(true);
    expect(factorsM(4).isNothing()).toBe(true);
    expect(factorsM(15).isNothing()).toBe(true);
  });
});
