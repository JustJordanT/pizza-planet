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
- [Repository pattern](https://www.martinfowler.com/eaaCatalog/repository.html)
- [MassTransit](https://masstransit-project.com/)
- [RabbitMQ](https://www.rabbitmq.com/)
- [JWT customer token generation](https://jwt.io/)
- [Pulumi Infrastructure Automation](https://www.pulumi.com/)
- [JWT customer token generation](https://jwt.io/)
- [PostgreSQL](https://www.postgresql.org/)

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

## Message Queue
In our API app, we streamline the ordering process by sending orders directly to the backend kitchen for processing. This is achieved by routing orders through an exchange, then being added to a queue for the kitchen to efficiently handle. Here are some reasons why I did want to use an intermediary exchange in RabbitMQ:

- Increased complexity: Adding an intermediary exchange increases the complexity of your messaging architecture, making it harder to understand and troubleshoot.

- Reduced performance: Intermediary exchanges add an additional hop for messages to pass through, which can slow down the overall performance of your system.

- Increased risk: Intermediary exchanges can act as a single point of failure, meaning that if they go down, your entire messaging system may be affected.

- Limited scalability: Intermediary exchanges can become a bottleneck if you have a high volume of messages passing through them, limiting the scalability of your system.

- Extra cost: Intermediary exchanges can add extra cost to your messaging infrastructure, as they require additional resources to operate

Our MQ flows would look something like the following.
<img width="1301" alt="image" src="https://user-images.githubusercontent.com/38886930/214488270-0fd43c3d-f660-4100-9e87-5e7722a6379c.png">

This flowchart describes a process for ordering and preparing a pizza using our APIs.

1. The Pizza API sends an order to the Order Exchange.
2. The Order Exchange adds the order to the Order Queue.
3. The Kitchen API consumes messages from the Order Queue, and processes the order.
4. The Kitchen API sends an update on the order's status to the Order Status Exchange.
5. The Order Status Exchange adds the status update to the Order Status Queue
6. The Pizza API consumes messages from the Order Status Queue to update the order status for the customer.

```mermaid
sequenceDiagram
Pizza API ->> Order Exchange: Send Order
Order Exchange ->> Order Queue: Add to Queue
Order Queue ->> Kitchen API: Consume Messages from queue
Kitchen API -->> Kitchen API: Process Order
Kitchen API -->> Order Status Exchange: Send Order Update Status
Order Status Exchange -->> Order Status Queue: Add to queue
Order Status Queue ->> Pizza API: Consume Messages from queue
```

