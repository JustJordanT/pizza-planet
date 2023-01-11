# pizza-planet-modern-monolith
Pizza Planet Modern Monolith Application 🍕 🛻

![image](https://user-images.githubusercontent.com/38886930/210684666-03c3544f-a177-48ec-8cf5-f16da8a2c0b0.png)

## High level design
<img width="1099" alt="image" src="https://user-images.githubusercontent.com/38886930/210846101-8af1aafb-a7f0-4f4c-a6b9-13dfe919cfa6.png">

## DB Diagram of Pizzas Planet
<img width="867" alt="image" src="https://user-images.githubusercontent.com/38886930/211888116-85f63990-15ba-4837-8d46-7f94c1407b1a.png">

## Tech Stack

- [FluentValidation](https://docs.fluentvalidation.net/en/latest/index.html)
- [BCrypt - for encrypting passwords at rest](https://www.nuget.org/packages/BCrypt.Net-Next)
- [MongoDB](https://www.mongodb.com/)
- [JWT customer token generation](https://jwt.io/)

## User Flow

1. A customer opens the app or website and they are able to submit a pizza to their cart for there order.

2. The customer selects one or more pizzas to add to their cart.

3. The customer can view their cart and make any necessary adjustments to their order before proceeding to checkout.

4. The customer enters their shipping information and selects a payment method to complete their order.

5. The order is then sent to the kitchen for preparation, and a unique order ID is generated.

6. The customer receives a notification that their order has been received and is being prepared.

7. Once the order is ready, it is sent for delivery to the customer's specified address.

8. The customer receives their order and an invoice with the order details and total cost.
