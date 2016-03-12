require 'factory_girl'
require 'pry'

class RecordLike
  def save!
  end
end

class Person < RecordLike
  attr_accessor :name, :age, :address
end

class Address < RecordLike
  attr_accessor :street, :city, :state
end

FactoryGirl.define do

  factory(:person) do
    name "Billy Smith"
    age 25
    address
  end

  factory(:address) do
    street "1234 Main St"
    city "Anytown"
    state "MI"
  end
end

include FactoryGirl::Syntax::Methods

describe 'factory' do
  it 'will work' do
    billy = create :person
    expect(billy.age).to eq(25)
  end
end

###############

code = "def add(x, y)
  x + y
end"


describe 'ast' do
  it 'will work' do
    ast = RubyVM::InstructionSequence.compile code
    #puts ast.to_a
    expect(ast.to_a[13][5][1][4][:arg_size]).to eq(2)

    ast.eval
    expect(add(3,4)).to eq(7)
  end
end
