require 'monad_maybe'

describe 'maybe monad' do
  it 'handles correct cases' do
    result = MaybeExample.maybe_func(5)
    expect(result).to eq(15)

    result = MaybeExample.maybe_func(-4)
    expect(result).to be nil
  end

  it 'stops early' do
    result = MaybeExample.maybe_func_stop_early(-4)
    expect(result).to be nil

    expect { MaybeExample.maybe_func_stop_early(5) }.to raise_exception("uh oh")
  end
end
