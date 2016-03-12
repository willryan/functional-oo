require 'piece_pipe'
require 'funkify'

class ChangeRatio < PiecePipe::AssemblyStep
  def receive(inputs)
    install ratio_adjusted: (inputs[:celcius] * 9/5.0)
  end
end

class AddOffset < PiecePipe::AssemblyStep
  def receive(inputs)
    install farenheit: (inputs[:ratio_adjusted] + 32.0)
  end
end

class UsePiecePipe
  def celsius_to_farenheit(temp)
    PiecePipe::Pipeline.new.
     source([{celcius: temp}]).
     step(ChangeRatio).
     step(AddOffset).
     collect(:farenheit).
     to_enum.
     first
  end
end

##########

class UseFunkify
  include Funkify

  auto_curry
  def add(x, y)
    x + y
  end

  def mult(x, y)
    x * y
  end
end

class FunkifyTemperature
  include Funkify

  def initialize
    @funcs = UseFunkify.new
  end

  def celsius_to_farenheit(temp)
    pass(temp) >=
      @funcs.mult(9/5.0) |
      @funcs.add(32.0)
  end

end
