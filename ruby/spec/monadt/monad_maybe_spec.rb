require 'monadt/monad_maybe'

describe 'maybe monad' do
  it 'handles correct cases' do
    result = Monadt::MaybeExample.maybe_func(5)
    expect(result).to eq(15)

    result = Monadt::MaybeExample.maybe_func(-4)
    expect(result).to be nil
  end

  it 'stops early' do
    result = Monadt::MaybeExample.maybe_func_stop_early(-4)
    expect(result).to be nil

    expect { Monadt::MaybeExample.maybe_func_stop_early(5) }.to raise_exception("uh oh")
  end
end