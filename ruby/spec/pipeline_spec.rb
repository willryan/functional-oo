require 'pipeline'

describe 'piece pipe' do
  it 'chains stuff' do
    pending
    raise 'todo'
  end
end

describe 'funkify pipeline' do
  let (:subject) { FunkifyTemperature.new }
  it 'chains stuff' do
    expect(subject.celsius_to_farenheight(0.0)).to eq(32.0)
    expect(subject.celsius_to_farenheight(-40.0)).to eq(-40.0)
    expect(subject.celsius_to_farenheight(100.0)).to eq(212.0)
  end
end
