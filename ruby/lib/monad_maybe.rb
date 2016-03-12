require 'pry'
require_relative 'monad'

def may1(x)
  x
end

def may2(z)
  if z > 0
    z * 2
  else
    nil
  end
end

def maybe_func(v)
  Monad.maybe do |y|
    x = y.yield may1 v
    y = y.yield may2 x
    x + y
  end
end

def maybe_func_stop_early(v)
  Monad.maybe do |y|
    x = y.yield may1 v
    y = y.yield may2 x
    raise 'uh oh'
    x + y
  end
end

describe 'maybe monad' do
  it 'handles correct cases' do
    result = maybe_func(5)
    expect(result).to eq(15)

    result = maybe_func(-4)
    expect(result).to be nil
  end

  it 'stops early' do
    result = maybe_func_stop_early(-4)
    expect(result).to be nil

    expect { maybe_func_stop_early(5) }.to raise_exception("uh oh")
  end
end
