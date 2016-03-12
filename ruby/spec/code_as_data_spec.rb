require 'code_as_data'

describe 'factory' do
  it 'will work' do
    billy = FactoryGirl.create :person
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
