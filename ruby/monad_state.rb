require 'pry'
require 'funkify'
require_relative 'monad'

class StateFuncs
  include Funkify

  auto_curry
  def add(num, state)
    [state+1, num+state]
  end
  def mult(num, state)
    [state*2, num*state]
  end
end

def state_func
  sf = StateFuncs.new
  ret = Monad.state(2) do |m|
    x = m.bind (sf.add 5)
    y = m.bind (sf.mult 3)
    m.bind (x + y)
  end
  puts "result: #{ret} == 17"
end

state_func
