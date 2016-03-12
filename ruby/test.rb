require 'rubygems'
require 'rspec'
require 'bundler/setup'
Bundler.require(:default)
Bundler.require(:test)


describe "some tests" do
  it "is a test" do
    Person = Hamsterdam::Struct.define(:name, :address, :age)
    david = Person.new(name: "David", age: true, address: "Coopersville")
    david1 = david.set_address("East Grand Rapids")
    david2 = david.merge(name: "Crosby", age: "increased")

    puts david1
    puts david2
  end
end
