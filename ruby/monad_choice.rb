require 'pry'
require_relative 'monad'

def choose1
  #Choice.failure "no"
  6
end

def choose2 z
  if z % 2 == 0
    z * 2
  else
    Choice.failure "i need even numbers"
  end
end

def stateful(num, state)
  [state+1, num*state]
end

def maybe_func
  ret = Monad.choice do |m|
    x = m.bind (choose1)
    y = m.bind (choose2 x)
    m.bind (x + y)
  end
  puts "result: #{ret}"
end

def maybe_func2
  ret = Monad.choice2 do
    x = yield choose1
    y = yield choose2 x
    yield(x + y)
  end
  puts "result: #{ret}"
end

maybe_func
