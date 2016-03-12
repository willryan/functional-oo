require 'mocha/test_unit'

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

describe 'function tests' do
  describe "#another_pure_function" do
    it 'can mock a static function' do
      StaticMethods.expects(:pure_function).with(10,16).returns(999)
      expect(MoreStaticMethods.another_pure_function(2, 8)).to eq(999)
    end
  end

  describe '#multiply_by_two' do
    it 'can make a proc from a method' do
      expect(MakeAProc.multiply_by_2 [1,2,3]).to eq([2,4,6])
    end
  end
end
