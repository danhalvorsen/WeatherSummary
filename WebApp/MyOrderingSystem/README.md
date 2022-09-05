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

- predict products for a given customer
- Basket
- Buy it
- Order history
- track order

Product

- See product details
- Customer wants to search for products
- administrator wants to see details
- administrator wants to search for products
- Administrator wants to change product details (price, title, description)

Inventory

- Register product
- Search products
- Inventory notification

Customer profile

- my page
- see order history
- Edit information

Reports

- see sales by date
- see orders completion
- see orders status
- see orderer information
- search for orders (date / orderer)
-
-

Campaigns

- see customers that given more 100 points
- bones

---

administrator should change the status of order (prepare / sent / delivered )
show the newts added products on the first page (separate part of page)

### Features

## User Stories

### Products feature

- As a User I want to see all product in the first page.
  - Create all products in a one page named Product List.
- As a User I want see the title, image and price for each product.
  - make an appropriate size for preview image.
- As a User I want click on product so that the next page will show detail of product

### Product detail feature

- As a User I want to see the product details like bigger image, title, description, price , availability and brand.
- As a User I need to add a product to my shopping list.
- As a User I want to increase or decrease number of products.

### Shopping list feature

- As a User I want to navigate to my shopping list
  - create an icon in the navigation bar
- As a User I want to see my shopping list.
- As a User I want to be able to increase or decrease number of each item.
- As a User I want to be able to remove some of items from the shopping list.
- As a User I want to see the total price.

### Search feature

- As a user, I want to be able to search for products name so that I can reach the specific product.
  - Search component
  - Load the search component in product list page to be able to search items.
  - Display two kinds of result to show, based on find item(partial list) or not find any item (empty view)

### Checkout feature

- As a User I want to submit my order.
- As a User I want to see appropriate message after submit my order so that I will be sure about the result of my order

### Navigation feature

- As a user I want to be able to go back to the Home screen (List items) so that I can see all the products
  - Use 'replace technic' in React router

# Backlog Technical

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

- As an Admin I want to see, add, edit and delete products
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
