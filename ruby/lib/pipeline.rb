require 'piece_pipe'
require 'funkify'

class UsePiecePipe
end

describe 'piece pipe' do
  it 'chains stuff' do
    pending
    raise 'todo'
  end
end

##########

class UseFunkify
  include Funkify

  auto_curry
  def add(x, y)
    x + y
  end

  def mult(x, y)
    x * y
  end
end

class FunkifyTemperature
  include Funkify

  def initialize
    @funcs = UseFunkify.new
  end

  def celsius_to_farenheight(temp)
    pass(temp) >=
      @funcs.mult(9/5.0) |
      @funcs.add(32.0)
  end

end

describe 'funkify pipeline' do
  let (:subject) { FunkifyTemperature.new }
  it 'chains stuff' do
    expect(subject.celsius_to_farenheight(0.0)).to eq(32.0)
    expect(subject.celsius_to_farenheight(-40.0)).to eq(-40.0)
    expect(subject.celsius_to_farenheight(100.0)).to eq(212.0)
  end
end
