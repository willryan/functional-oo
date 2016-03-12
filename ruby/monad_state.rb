require 'pry'
require 'funkify'
require_relative 'monad'

class Array
  def rest
    self[1..-1]
  end
end

class Lang
  def self.english
    ['zero', 'one', 'two', 'three', 'four', 'five']
  end
  def self.spanish
    ['cero', 'uno', 'dos', 'tres', 'quatro', 'cinco']
  end
end

class StateFuncs
  include Funkify

  auto_curry
  def unpack_english(base, state)
    v = Lang.english[state.first % base]
    [v, state.rest]
  end

  def unpack_spanish(base, state)
    v = Lang.spanish[state.first % base]
    [v, state.rest]
  end
end

def state_func(bytes)
  sf = StateFuncs.new
  Monad.state(bytes) do |m|
    a = m.bind (sf.unpack_english 4)
    b = m.bind (sf.unpack_english 6)
    c = m.bind (sf.unpack_spanish 5)
    d = m.bind (sf.unpack_spanish 2)
    m.bind "#{a} and #{b} + #{c} y #{d}"
  end
end

describe 'state monad' do
  it 'can do deserialization' do
    expect(state_func([6,11,8,10])).to eq("two and five + tres y cero")
  end
end
