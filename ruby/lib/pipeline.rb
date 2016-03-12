require 'piece_pipe'
require 'funkify'

class UsePiecePipe
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

  def celsius_to_farenheight(temp)
    pass(temp) >=
      @funcs.mult(9/5.0) |
      @funcs.add(32.0)
  end

end
