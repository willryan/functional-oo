require 'pry'

class Enumerator::Yielder
  alias bind yield
end

class Choice
  attr_reader :success_value, :failure_value

  def is_success
    @success_value != nil
  end

  def self.success(val)
    Choice.new val, nil
  end

  def self.failure(val)
    Choice.new nil, val
  end

  def to_s
    if is_success
      "success: #{success_value}"
    else
      "failure: #{failure_value}"
    end
  end

  private
  def initialize(success_value, failure_value)
    @success_value = success_value
    @failure_value = failure_value
  end
end

class Monad
  class << self

    def maybe(&blk)
      e = Enumerator.new(&blk)
      e.each do |itm|
        if itm == nil
          break
        else
          itm
        end
      end
    end

    def state(initial_state, &blk)
      e = Enumerator.new(&blk)
      state = initial_state
      e.each do |itm|
        unless itm.is_a? Proc
          raw_value = itm
          itm = ->(st) { [raw_value, st] }
        end
        value, state = itm.call(state)
        value
      end
    end

    def reader(env, &blk)
      e = Enumerator.new(&blk)
      e.each do |itm|
        itm.call(env)
      end
    end

    def reader_state_choice(env, initial_state, &blk)
      e = Enumerator.new(&blk)
      acc = nil
      state = initial_state
      e.each do |itm_f|
        if itm_f.is_a? Proc
          itm, state = itm_f.call(env, state)
        else
          itm = itm_f
        end
        unless itm.is_a? Choice
          itm = Choice.success itm
        end
        acc = itm
        if itm.is_success
          itm.success_value
        else
          break
        end
      end
      acc
    end

    def choice(&blk)
      e = Enumerator.new(&blk)
      acc = nil
      e.each do |itm|
        unless itm.is_a? Choice
          itm = Choice.success itm
        end
        acc = itm
        if itm.is_success
          itm.success_value
        else
          break
        end
      end
      acc
    end

    def choice2(&blk)
      e = Enumerator.new(&blk)
      acc = nil
      while true
        begin
          itm = e.next_values[0]
        rescue StopIteration
          return $!.result
        end
        unless itm.is_a? Choice
          itm = Choice.success itm
        end
        acc = itm
        if !acc.is_success
          return acc
        end
        y = yield(acc.success_value)
        e.feed y
      end
      acc
    end
  end
end

