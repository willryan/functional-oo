require 'pry'
require_relative 'monad'

def choose1(x)
  #Choice.failure "no"
  if x > 5
    x - 5
  else
    Choice.failure "less than 5"
  end
end

def choose2(z)
  if z % 2 == 0
    z * 2
  else
    Choice.failure "i need even numbers"
  end
end

def choice_func(v)
  Monad.choice do |m|
    x = m.bind (choose1 v)
    y = m.bind (choose2 x)
    m.bind (x + y)
  end
end

def choice_func_stop_early(v)
  Monad.choice do |m|
    x = m.bind (choose1 v)
    raise 'uh oh'
    y = m.bind (choose2 x)
    m.bind (x + y)
  end
end

def choice_func2
  Monad.choice2 do
    x = yield choose1
    y = yield choose2 x
    yield(x + y)
  end
end

describe 'choice monad' do
  it 'handles correct cases' do
    result = choice_func(9)
    expect(result.is_success).to be true
    expect(result.success_value).to eq(12)

    result = choice_func(3)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("less than 5")

    result = choice_func(10)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("i need even numbers")
  end

  it 'stops early' do
    result = choice_func_stop_early(3)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("less than 5")

    expect { choice_func_stop_early(9) }.to raise_exception("uh oh")
  end
end
