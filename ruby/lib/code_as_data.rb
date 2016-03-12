require 'factory_girl'

class RecordLike
  def save!
  end
end

class Person < RecordLike
  attr_accessor :name, :age, :address
end

class Address < RecordLike
  attr_accessor :street, :city, :state
end

FactoryGirl.define do

  factory(:person) do
    name "Billy Smith"
    age 25
    address
  end

  factory(:address) do
    street "1234 Main St"
    city "Anytown"
    state "MI"
  end
end

