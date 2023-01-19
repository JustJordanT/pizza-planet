# pizza-planet-modern-monolith
[![wakatime](https://wakatime.com/badge/user/dd5a4963-3f0c-406d-b3f9-b374ef837ced/project/1fcfa5b0-04f5-45c7-8bb7-8ed5057b8f4b.svg)](https://wakatime.com/badge/user/dd5a4963-3f0c-406d-b3f9-b374ef837ced/project/1fcfa5b0-04f5-45c7-8bb7-8ed5057b8f4b)

Pizza Planet Modern Monolith Application 🍕 🛻

![image](https://user-images.githubusercontent.com/38886930/210684666-03c3544f-a177-48ec-8cf5-f16da8a2c0b0.png)

## High level design
<img width="801" alt="image" src="https://user-images.githubusercontent.com/38886930/213477773-25852e4e-c09b-4d77-af09-7fc08862d295.png">

## DB Diagram of Pizzas Planet
<img width="453" alt="image" src="https://user-images.githubusercontent.com/38886930/212588448-e32f370b-57e0-4ed1-a15a-6d642c3183c8.png">

## DB Diagram of Pizza Planet Kitchen
<img width="929" alt="image" src="https://user-images.githubusercontent.com/38886930/213474572-d3191743-b013-47c7-9b16-649dddf4638f.png">

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

