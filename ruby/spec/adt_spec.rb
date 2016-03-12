require 'adt'

describe 'proc/block based ADTs' do
  let(:v1) { TestAdt::One.new 1, :five }
  let(:v2) { TestAdt::Two.new "hoi" }
  let(:v3) { TestAdt::Three.new 6 }
  let(:subject) { UseAdts.new }

  it 'supports procs' do
    expect(subject.adt_func(v1)).to eq("1five")
    expect(subject.adt_func(v2)).to eq("default")
    expect(subject.adt_func(v3)).to eq(16)
  end

  it 'supports blocks' do
    expect(subject.adt_func2(v1)).to eq("1five")
    expect(subject.adt_func2(v2)).to eq("default")
    expect(subject.adt_func2(v3)).to eq(16)
  end
end

