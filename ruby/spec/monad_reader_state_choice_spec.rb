require 'monad_reader_state_choice'

describe 'reader state choice monad' do
  it 'handles correct cases' do
    val = ReaderStateChoiceExample.state_func(:english, [5,14,8])
    expect(val.success_value).to eq("one, five, two")
    val = ReaderStateChoiceExample.state_func(:spanish, [5,14,8])
    expect(val.success_value).to eq("uno, cinco, dos")
  end

  it 'stops early' do
    val = ReaderStateChoiceExample.state_func(:english, [5,8,8])
    expect(val.failure_value).to eq("too big")
  end
end
