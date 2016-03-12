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
  def unpack(base, language, state)
    modded = state.first % base
    if (modded > 5)
      [Choice.failure('too big'), state.rest]
    else
      v = Lang.send(language)[modded]
      [Choice.success(v), state.rest]
    end
  end
end

def state_func(language, bytes)
  sf = StateFuncs.new
  Monad.reader_state_choice(language, bytes) do |m|
    x = m.bind (sf.unpack 4)
    y = m.bind (sf.unpack 9)
    z = m.bind (sf.unpack 3)
    m.bind "#{x}, #{y}, #{z}"
  end
end

describe 'reader state choice monad' do
  it 'handles correct cases' do
    expect(state_func(:english, [6,11,8]).success_value).to eq("one, three, five")
    expect(state_func(:spanish, [6,11,8]).success_value).to eq("uno, tres, cinco")
  end

  it 'stops early' do
    pending
    raise 'not yet'
  end
end
