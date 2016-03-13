//  yield abuse
require('babel-polyfill');

function Just(value) {
  this.value = value;
}

Just.prototype.bind = function(transform) {
  return transform(this.value);
};

Just.prototype.toString = function() {
  return 'Just(' +  this.value + ')';
};

const Nothing = {
  bind: function() {
    return this;
  },
  toString: function() {
    return 'Nothing';
  }
};

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

const factor = function (num, mod) {
  if (num % mod == 0) {
    return new Just(num / mod);
  } else {
    return Nothing;
  }
};

const factors = function (num) {
  return new Just(num).bind(x =>
    factor(x, 2).bind(y =>
      factor(y, 3).bind(z =>
        factor(z, 5))));
}

const factorsM = function (num) {
  return doM(function*() {
    var x = yield new Just(num);
    var y = yield factor(x, 2);
    var z = yield factor(y, 3);
    return factor(z, 5);
  }());
}

describe('maybe monad', () => {
  it('binds', () => {
    expect(factors(1)).toBe(Nothing);
    expect(factors(4)).toBe(Nothing);
    expect(factors(15)).toBe(Nothing);
    expect(factors(30).value).toBe(1);
    expect(factors(420).value).toBe(14);
  });

  it('can use syntactic sugar', () => {
    expect(factorsM(420).value).toBe(14);
    expect(factorsM(30).value).toBe(1);
    expect(factorsM(1)).toBe(Nothing);
    expect(factorsM(4)).toBe(Nothing);
    expect(factorsM(15)).toBe(Nothing);
  });
});

//  monet?
describe('monet', () => {
  xit('is another way to do monads', () => {
  });
});
