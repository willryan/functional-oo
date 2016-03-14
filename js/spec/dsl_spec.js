const variables = {};
let steps = [];

const addStep = function(f) {
  steps.push(f);
};

const clearSteps = function() {
  steps = [];
}

const set = function(name, value) {
  addStep(() => {
    variables[name] = value;
  });
};

const add = function(x,y,z) {
  addStep(() => {
    variables[z] = variables[x] + variables[y];
  });
};

const mult = function(x,y,z) {
  addStep(() => {
    variables[z] = variables[x] * variables[y];
  });
};

const andThen = function(f) {
  addStep(() => f(variables));
}

const evaluate = function(generateSteps) {
  generateSteps();
  for (var step of steps) {
    step();
  }
  clearSteps();
}

describe('dsl', () => {
  it('can be similar to Ember acceptance tests', () => {
    let tFarenheit = 0.0;
    evaluate(() => {
      set('tCelcius', 100);
      set('ratio', 9.0/5.0);
      set('offset', 32);
      mult('tCelcius','ratio','var1');
      add('var1','offset','tFarenheit');
      andThen((v) => {
        tFarenheit = v.tFarenheit;
      });
    });
    expect(tFarenheit).toBe(212.0);
  });
});

//wisp?
