require 'monad_choice'

describe 'choice monad' do
  it 'handles correct cases' do
    result = ChoiceExample.choice_func(9)
    expect(result.is_success).to be true
    expect(result.success_value).to eq(12)

    result = ChoiceExample.choice_func(3)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("less than 5")

    result = ChoiceExample.choice_func(10)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("i need even numbers")
  end

  it 'stops early' do
    result = ChoiceExample.choice_func_stop_early(3)
    expect(result.is_success).to be false
    expect(result.failure_value).to eq("less than 5")

    expect { ChoiceExample.choice_func_stop_early(9) }.to raise_exception("uh oh")
  end
end
