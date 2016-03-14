
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

const registry = {
  add(x, y) {
    return x + y;
  },
  mult(x, y) {
    return x * y;
  },
  sqrt(x) {
    return Math.sqrt(x);
  },
  get(name) {
    return this[name];
  },
  service() { return 'SPECIALVALUE'; },
  provideServices(obj) {
    for (const k in obj) {
      if (obj[k] == this.service()) {
        obj[k] = this.get(k);
      }
    }
  }
};

describe('DI example', () => {
  it('shows partial application through IOC', () => {
    const mathFuncs = {
      add: registry.service(),
      mult: registry.service(),
      sqrt: registry.service(),
      pythag(x,y) {
        return this.sqrt(this.add(this.mult(x,x),this.mult(y,y)));
      }
    }
    registry.provideServices(mathFuncs);

    expect(mathFuncs.pythag(3.0,4.0)).toBe(5.0);
  });
});
