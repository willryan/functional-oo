require 'rubygems'
require 'rspec'
require 'bundler/setup'
Bundler.require(:default)
Bundler.require(:test)

describe 'hamsterdam' do
  it 'allows for true immutability' do
    Person = Hamsterdam::Struct.define(:name, :address, :age)
    david = Person.new(name: "David", age: true, address: "Coopersville")
    david1 = david.set_address("East Grand Rapids")
    david2 = david.merge(name: "Crosby", age: "increased")

    puts david1
    puts david2

#     same_as_david = Person.new(name: "David", age: true, address: "Coopersville")
#
#     expect(david).not.to eq(david1)
#     expect(david1).not.to eq(david2)
#     expect(david).not.to eq(same_as_david)
#
#     expect(david.name).to eq("David")
#     expect { david.name = 'foo' }.to raise_exception
  end
end
