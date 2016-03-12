require 'functions'
require 'mocha/api'
include Mocha::API

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
