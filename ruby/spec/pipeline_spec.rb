require 'pipeline'

describe 'piece pipe' do
  let (:subject) { UsePiecePipe.new }
  it 'chains stuff' do
    expect(subject.celsius_to_farenheit(0.0)).to eq(32.0)
    expect(subject.celsius_to_farenheit(-40.0)).to eq(-40.0)
    expect(subject.celsius_to_farenheit(100.0)).to eq(212.0)
  end
end

describe 'funkify pipeline' do
  let (:subject) { FunkifyTemperature.new }
  it 'chains stuff' do
    expect(subject.celsius_to_farenheit(0.0)).to eq(32.0)
    expect(subject.celsius_to_farenheit(-40.0)).to eq(-40.0)
    expect(subject.celsius_to_farenheit(100.0)).to eq(212.0)
  end
end
