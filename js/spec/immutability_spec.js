const Immutable = require('immutable');

const data = {
  a: 1,
  b: 2
}

describe('immutability', () => {
  it ('creates copies on changes', () => {
    const map1 = Immutable.Map(data);
    const map2 = map1.set('b', 50);
    expect(map1.get('b')).toBe(2);
    expect(map2.get('b')).toBe(50);
  });
});

