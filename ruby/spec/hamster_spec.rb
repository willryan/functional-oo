require 'hamsterdam'

describe 'hamsterdam' do
  it 'allows for true immutability' do
    Dude = Hamsterdam::Struct.define(:name, :address, :age)
    david = Dude.new(name: "David", age: true, address: "Coopersville")
    expect(david.name).to eq("David")
    expect { david.name = 'foo' }.to raise_exception(NoMethodError)

    david1 = david.set_address("East Grand Rapids")
    expect(david).not_to eq(david1)

    david2 = david.merge(name: "Crosby", age: "increased")
    expect(david1).not_to eq(david2)

    same_as_david = Dude.new(name: "David", age: true, address: "Coopersville")
    expect(david).to eq(same_as_david)
  end
end
