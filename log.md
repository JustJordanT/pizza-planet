# LOG

- [January 12, 2023] Made a dumb choise to pick mongodb just to try it out, but due to this project this not very suitable since we have a clear path on the relationships.
- Migration all controllers and repositories over to this new dbContext using postgres
- [January 13, 2023]: Well.... I had to start over from stratch due to some poor choices. because I was using nosql and forcing relationships. this caused my relationships to be a little wonky when I did switch over to SQL. and I also saw places where I would be able to better improve on the flow of the application as well. Going to be working on the following flow.
  - When a user is created. a cart is also created for them to add items, we will also create a order for them as well.
  - When a pizzas are created under a customer these will be added automatically to the cart.
  - Pricing will be updated all the way through as well.
  - When a customer is done they will mark the pay and then the `order` will placed into a `pending` status this is when we will send this to the `queue` for the kitchen backend to pick up the order.
  - Once a `order` is completed then the customer will get a email letting them know that a order is completed and ready for pickup.
  - **Rough Draft^^**
