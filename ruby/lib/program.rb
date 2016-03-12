require 'hamsterdam'

Person = Hamsterdam::Struct.define(:name, :address, :age)
david = Person.new(name: "David", age: true, address: "Coopersville")
david1 = david.set_address("East Grand Rapids")
david2 = david.merge(name: "Crosby", age: "increased")

puts david1
puts david2
