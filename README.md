# pizza-planet-modern-monolith
[![wakatime](https://wakatime.com/badge/user/dd5a4963-3f0c-406d-b3f9-b374ef837ced/project/1fcfa5b0-04f5-45c7-8bb7-8ed5057b8f4b.svg)](https://wakatime.com/badge/user/dd5a4963-3f0c-406d-b3f9-b374ef837ced/project/1fcfa5b0-04f5-45c7-8bb7-8ed5057b8f4b)

Pizza Planet Modern Monolith Application 🍕 🛻

![image](https://user-images.githubusercontent.com/38886930/210684666-03c3544f-a177-48ec-8cf5-f16da8a2c0b0.png)

## High level design
<img width="1099" alt="image" src="https://user-images.githubusercontent.com/38886930/210846101-8af1aafb-a7f0-4f4c-a6b9-13dfe919cfa6.png">

## DB Diagram of Pizzas Planet
<img width="867" alt="image" src="https://user-images.githubusercontent.com/38886930/211888116-85f63990-15ba-4837-8d46-7f94c1407b1a.png">

## Tech Stack

- [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [FluentValidation](https://docs.fluentvalidation.net/en/latest/index.html)
- [BCrypt - for encrypting passwords at rest](https://www.nuget.org/packages/BCrypt.Net-Next)
- [MongoDB](https://www.mongodb.com/)
- [JWT customer token generation](https://jwt.io/)

## User Flow

- Customers start by browsing the menu of pizzas available through the API. 
- They can filter or search for specific pizzas, and view details about each one.
- Once the customer has decided on the pizzas they want to order, they add them to their cart. 
- The cart is connected to their account, so they can save it for later or come back to it.
- Next, the customer reviews the items in their cart and can make changes if needed. Once they're satisfied, they proceed to checkout.
- During checkout, the customer make the payment, the order get confirm and they receive the confirmation

```mermaid
graph TD;
    Customers-->|browse menu|Pizzas;
    Pizzas-->|add to cart|Cart;
    Cart-->|review and edit|Cart;
    Cart-->|checkout|Orders;
    Orders-->|Payment| Orders;
    Orders-->|Confirmation|Customers;
```

