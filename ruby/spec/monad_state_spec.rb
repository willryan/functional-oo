require 'monad_state'

describe 'state monad' do
  it 'can do deserialization' do
    expect(StateExample.state_func([6,11,8,10])).to eq("two and five + tres y cero")
  end
end