# Summary

- We need to system where customers can buy products from us
- We need a system where we can have control over what we have in store and what we need to order from suppliers
- We need a bonus program for our "best" customers
- We need control over 3rd parties suppliers prices (We have to avoid paying more than we need.)
- Reporting
  - Selling
  - Inventory
  - etc.

We want well designed web pages (In React)
(Make a design and get approval before any coding)

## EPICS

Order

- Basket (1)

  - As a customer, I want to navigate to my basket so that I can go to my shopping list. (3d)

  - As a customer, I want to see how many items are in my basket so that I can see the number of products in my basket (1d)
    <!-- - We need to put a badge to show numbers of products in the basket and sync it with state.
    - after buying items, this badge should be clean up.
    - number of items in badge should be state on browser local storage(a solution). -->
  - As a customer, I want to see the contents of my basket so that I can see the products I wanted to buy (1d)
    <!-- - by click on basket it should go to shopping list so we need a shopping list component
    - show another product view in shopping list and calculate its amount -->
    -
  - As a customer, I want to be able to increase or decrease the number of each item so that I can change the number of each product I am going to buy. (1d)
    <!-- - create + and - icon and show the number of product.
    - update data follow stream  -->
  - As a customer, I want to be able to remove items from the basket so that if I regret buying them, I can remove them. (6h)
  <!-- - create 'Remove' icon to manage it
  - -->
  - As a customer, I want to see the total price so that before finalizing the order I can see how much should I pay (4h)

- Buy it (2)
- Order history (3)
- track order (3)
- predict products for a given customer (3)

Product

- See product (1)

  - As a customer, I want to see products in the home page so that I can find product directly.(3d)
    - connect to database(mock data) and get data'
    -
  - As a customer, I want to see the title, image and price for each product so that I have an overall concept of each product
  - As a customer, I want click on product so that the next page will show detail of product.(2d)
  - As a customer, I want to navigate to the home page from any pages I am on, so that I can reach the home page directly

- see details (1)

  - As a customer, I want to see the product details with a title, description, price, availability, brand and bigger image. (3d)
  - As a customer, I want to add a product to my basket so that I can aggregate the products I am going to buy them in one place
  - As a customer, I want to increase or decrease the number of products so that I can change the number of items I want to buy. (2d)

- search for products (1)

  - As a customer, I want to see a search box on top of the products list so that I can easily search for products. (3d)
  - As a customer, I want to be able to search for products name so that I can reach the specific product.

Inventory

- Register product (1)

  - As an admin, I want to register new products so that I can add a new product to the store (3d)
  - As an admin, I want to edit products, so that I can change the specifics of products (2d)

- Inventory notification (3)

Reports

- see sales (1)

  - As an admin, I want to see the list of orders so that I can find out how many orders I've got. (2d)
  - As an admin, I want to click on each item of order and see its detail so that I can see more of details.(1d)

- see orders completion (3)
- see orders status (3)
- see orderer information (3)
- search for orders (date / orderer) (3)

Customer profile

- my page (1) (??)

  - As a customer I want to see my page so that I can (???????)

- create profile (1) (??)

  - As a customer I want to register as a new customer so that I have a profile in the store.

- Delete profile (1)(??)

  - As a customer I want to be able to delete my profile so that I can decided to have profile in this system or not.
  - As an admin I want to be able to delete a profile so that I can delete the fake profiles

- see order history (2)
- Edit information (3)

Campaigns

- see customers that given more 100 points (2)
- bones (2)

- roles

### Features

## User Stories

### Checkout feature

- As a User I want to submit my order.
- As a User I want to see appropriate message after submit my order so that I will be sure about the result of my order

### Navigation feature

- As a user I want to be able to go back to the Home screen (List items) so that I can see all the products
  - Use 'replace technic' in React router

# Backlog Technical

Load the search component in product list page to be able to search items.
Display two kinds of result to show, based on find item(partial list) or not find any item (empty view)

### Task

- Create structure of date that we need to keep products
- Create structure of date that we need to store each order
- Create Json files regarding our data structures
- Design blue print of each page and component

### Setup DB

As a frontend developer I need “ Fake data for products”

- It should be a json file or json server in this phase

### Tests for some functions

- Unit test should be present where is needed.

## EPICS (Phase2)

### Features

- Inventory
- Authentication
- Profile

### Comment feature (Phase 2)

- As a User I want comment for a product so that I can give my comment and read other comments for each product - Should validate for appropriate input text

## Inventory management by Admin feature (Phase 2)

- ### Reporting feature (Phase 2)

- As an admin I want to see the report of orders so that I can see all the orders.
- As an admin I want to see the remaining Products.
- As an admin I want to see stock of products so that I can get a list to order to suppliers.

# Backlog Functional

- Data should be validated and stored in a browser database (We will later on sync data from this database to backend)
- Should use Route between pages.
- Use local storage in browser to maintain state of orders in shopping list.
- Use Json mock data for products and orders at the first (first phase)
- Unit test is important! Unit test should be present where is needed.
- Data should be synced to a MongoDb database (Through a small NodeJS)
    <!-- [link](<(https://github.com/Automattic/mongoose)>)
    (The synchronization should be executed each hour) -->

# Backlog Technical
