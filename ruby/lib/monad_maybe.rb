require 'pry'
require_relative 'monad'

class MaybeExample
  class << self
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
  end
end
