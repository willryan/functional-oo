require 'pry'

Case1 = Class.new
Case2 = Struct.new :foo
Case3 = Struct.new :bar, :baz

def longhand(adt)
  case adt
  when Case1
    1
  when Case2
    adt.foo
  when Case3
    adt.bar * adt.baz
  end
end

p (longhand Case2.new 5)
p (longhand Case1.new)
p (longhand Case3.new 2, 7)

###################

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

def match(o, *cases)
  m = cases.find do |tpl|
    tpl.klass == o.class || tpl.klass == Default
  end
  m.lambda.call(*(o.values.take(m.lambda.arity)))
end

def with(klass, prc=nil, &blk)
  AdtPattern.new klass, prc || blk
end

module TestAdt
  One = data :foo, :bar
  Two = data :foo
  Three = data :baz
end

def adt_func2(o)
  match o,
    with(TestAdt::One) { |foo, bar| foo.to_s + bar.to_s },
    with(TestAdt::Three) { |foo| foo + 10 },
    with(Default) { "default" }
end

def adt_func(o)
  match o,
    with(TestAdt::One, ->(foo, bar) { foo.to_s + bar.to_s }),
    with(TestAdt::Three, ->(foo) { foo + 10 }),
    with(Default, ->() { "default" })
end

obj1 = TestAdt::One.new 1, :five
obj2 = TestAdt::Two.new "hoi"
obj3 = TestAdt::Three.new 6

puts adt_func(obj1)
puts adt_func(obj2)
puts adt_func(obj3)

puts adt_func2(obj1)
puts adt_func2(obj2)
puts adt_func2(obj3)
