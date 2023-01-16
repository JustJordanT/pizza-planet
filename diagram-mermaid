```mermaid
classDiagram
direction BT
class CartEntity {
   numeric Price
   integer Quantity
   text CustomerId
   boolean IsActive
   timestamp with time zone CreatedAt
   timestamp with time zone UpdatedAt
   text PizzaId
   text Id
}
class CustomerEntity {
   text Name
   text Email
   text Password
   text PasswordHash
   text PasswordSalt
   timestamp with time zone CreatedAt
   timestamp with time zone UpdatedAt
   text Id
}
class OrderEntity {
   text CartId
   text OrderStatus
   timestamp with time zone CreatedAt
   timestamp with time zone UpdatedAt
   text Id
}
class PizzasEntity {
   text CrustType
   text Size
   numeric Price
   text[] Toppings
   boolean IsGlutenFree
   boolean IsVegan
   boolean IsVegetarian
   integer Quantity
   text CartId
   timestamp with time zone CreatedAt
   timestamp with time zone UpdatedAt
   text Id
}

CartEntity  -->  CustomerEntity : CustomerId:Id
OrderEntity  -->  CartEntity : CartId:Id
PizzasEntity  -->  CartEntity : CartId:Id
```
