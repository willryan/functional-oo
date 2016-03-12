require 'pry'
require 'funkify'
require_relative 'monad'

class StateFuncs
  include Funkify

  auto_curry
  def add(num, env, state)
    if state < 5
      [Choice.success(env+state+1), num+state]
    else
      Choice.failure "and i'm out"
    end
  end
  def mult(num, env, state)
    if state % 3 == 0
      Choice.failure "no threes"
    else
      [env+state*2, num*state]
    end
  end
end

def state_func
  sf = StateFuncs.new
  ret = Monad.reader_state_choice(1, 2) do |m|
    x = m.bind (sf.add 5)
    y = m.bind (sf.mult 3)
    m.bind (x + y)
  end
  puts "result: #{ret} == 19"
end

describe 'reader state choice monad' do
  it 'handles correct cases' do
    pending
    raise 'not yet'
  end

  it 'stops early' do
    pending
    raise 'not yet'
  end
end
