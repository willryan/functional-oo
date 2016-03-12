AdtPattern = Struct.new :klass, :lambda

def data(*fields)
  base = if fields.size > 0
           Struct.new(*fields)
         else
           Object
         end
  Class.new(base)
end

Default = data

module Adt
  def match(o, *cases)
    m = cases.find do |tpl|
      tpl.klass == o.class || tpl.klass == Default
    end
    m.lambda.call(*(o.values.take(m.lambda.arity)))
  end

  def with(klass, prc=nil, &blk)
    AdtPattern.new klass, prc || blk
  end
end


module TestAdt
  One = data :foo, :bar
  Two = data :foo
  Three = data :baz
end

class UseAdts
  include Adt

  def adt_func(o)
    match o,
      with(TestAdt::One, ->(foo, bar) { foo.to_s + bar.to_s }),
      with(TestAdt::Three, ->(foo) { foo + 10 }),
      with(Default, ->() { "default" })
  end

  def adt_func2(o)
    match o,
      with(TestAdt::One) { |foo, bar| foo.to_s + bar.to_s },
      with(TestAdt::Three) { |foo| foo + 10 },
      with(Default) { "default" }
  end
end

