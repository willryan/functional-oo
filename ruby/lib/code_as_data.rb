require 'factory_girl'

FactoryGirl.define do
  factory(:person) do
    name "Billy Smith"
    age 25
    spouse
  end
end

include FactoryGirl::Syntax::Methods

describe 'factory' do
  it 'will work' do
    pending
    raise 'todo'
  end
end

###############

code = "def add(x, y)
  x + y
end"

ast = RubyVM::InstructionSequence.compile code

describe 'ast' do
  it 'will work' do
    pending
    raise 'todo'
  end
end
