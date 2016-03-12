Case1 = Class.new
Case2 = Struct.new :foo
Case3 = Struct.new :bar, :baz

def longhand(adt)
  case adt
  when Case1
    1
  when Case2
    adt.foo
  when Case3
    adt.bar * adt.baz
  end
end

describe 'longhand adt' do
  it 'detects cases' do
    expect(longhand(Case2.new 5)).to eq(5)
    expect(longhand(Case1.new)).to eq(1)
    expect(longhand(Case3.new 2, 7)).to eq(14)
  end
end
