require 'pry'
require_relative 'monad'

def may1
  5
end

def may2 z
  z * 2
end

def maybe_func
  ret = Monad.maybe do |y|
    x = y.yield may1
    y = y.yield may2 x
    x + y
  end
  puts "result: #{ret}"
end

maybe_func
