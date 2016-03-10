class StaticMethods
  class << self
    def pure_function(argument1, argument2)
      # signifies pure, but you can call other static functions or use @@
      arg1 + arg2
    end
  end
end

class MoreStaticMethods
  class << self
    def another_pure_function(arg1, arg2)
      StaticMethods.pure_function(arg1 + arg2, arg1 * arg2)
    end
  end
end

class MakeAProc
  class << self
    def multiply_by_2(array)
      array.map(&method(:mul2))
    end

    def mul2(x)
      x * 2
    end
  end
end

p (MakeAProc.multiply_by_2 [1,2,3])

# test that fakes out dependency
