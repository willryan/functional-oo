require 'hamsterdam'

describe 'hamsterdam' do
  it 'allows for true immutability' do
    Dude = Hamsterdam::Struct.define(:name, :address, :age)
    david = Dude.new(name: "David", age: true, address: "Coopersville")
    david1 = david.set_address("East Grand Rapids")
    david2 = david.merge(name: "Crosby", age: "increased")

    same_as_david = Dude.new(name: "David", age: true, address: "Coopersville")

    expect(david).not_to eq(david1)
    expect(david1).not_to eq(david2)
    expect(david).to eq(same_as_david)

    expect(david.name).to eq("David")
    expect { david.name = 'foo' }.to raise_exception(NoMethodError)
  end
end
