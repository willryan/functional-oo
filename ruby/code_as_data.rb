require 'factorygirl'

factory(:person) do
  name "Billy Smith"
  age 25
  spouse
end

###############

code = "def add(x, y)
  x + y
end"

ast = RubyVM::InstructionSequence.compile code

# TODO
