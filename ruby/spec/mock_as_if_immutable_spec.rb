require 'mocha/api'
include Mocha::API

class DoMutableStuff
  # this method returns the input record and not a new copy,
  # but we can mock it as though the output might be
  def self.update(record, attributes)
    record.update attributes
    record.save
    record
  end
end

class CallsMutableStuff
  # both versions will work with the above code,
  # but not with the mock
  def self.update_as_though_immutable(record, new_status)
    updated = DoMutableStuff.update record, status: new_status
    updated.status
  end

  def self.update_as_though_mutable(record, new_status)
    DoMutableStuff.update record, status: new_status
    record.status
  end
end

describe 'mock as if mutable' do
  it 'requires called code to be treated as pure' do
    rec1 = mock('1')
    rec1.stubs(:status).returns('bad')
    rec2 = mock('2')
    rec2.stubs(:status).returns('fine')
    DoMutableStuff.stubs(:update).with(rec1, status: 'ok').returns(rec2)

    expect(CallsMutableStuff.update_as_though_immutable(rec1, 'ok')).to eq('fine')

    expect(CallsMutableStuff.update_as_though_mutable(rec1, 'ok')).to eq('bad')
  end
end
