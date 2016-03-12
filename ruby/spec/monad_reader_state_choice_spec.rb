require 'monad_reader_state_choice'

describe 'reader state choice monad' do
  it 'handles correct cases' do
    expect(ReaderStateChoiceExample.state_func(:english, [5,14,8]).success_value).to eq("one, five, two")
    expect(ReaderStateChoiceExample.state_func(:spanish, [5,14,8]).success_value).to eq("uno, cinco, dos")
  end

  it 'stops early' do
    expect(ReaderStateChoiceExample.state_func(:english, [5,8,8]).failure_value).to eq("too big")
  end
end
