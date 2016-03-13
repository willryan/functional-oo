const adt = require('adt');

const Either = adt.data({
  Left: { error: adt.only(String) },
  Right: { value: adt.any }
});

const eitherFunc = function(x) {
  if (x < 10) {
    return Either.Left('too small');
  } else if (x % 2 == 0) {
    return Either.Left('even');
  } else {
    return Either.Right(x - 1);
  }
};

const mapEither = function(e) {
  if (e.isLeft) {
    return e.set({error: 'no good'});
  } else {
    return e.set({value: e.value * 2});
  }
}

describe('adt', () => {
  it('can be done, but does not look great', () => {
    var result = eitherFunc(3);
    expect(result.isLeft).toBe(true);
    expect(result.error).toBe('too small');

    result = eitherFunc(32);
    expect(result.isLeft).toBe(true);
    expect(result.error).toBe('even');

    result = eitherFunc(13);
    expect(result.isRight).toBe(true);
    expect(result.value).toBe(12);

    result = mapEither(eitherFunc(32));
    expect(result.isLeft).toBe(true);
    expect(result.error).toBe('no good');

    result = mapEither(eitherFunc(13));
    expect(result.isRight).toBe(true);
    expect(result.value).toBe(24);
  });
});
