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

# Backlog Functional

<!-- - Data should be validated and stored in a browser database (We will later on sync data from this database to backend) -->

- Unit test is important! Unit test should be present where is needed
  <!-- - Data should be synced to a MongoDb database (Through a small NodeJS) -->
    <!-- [link](<(https://github.com/Automattic/mongoose)>)
    (The synchronization should be executed each hour) -->

## EPICS

Ordering system

### Features 3 days

Ordering page
++ product lists
++ product detail
++ Inventory
++

#### User Stories

- As a user I need a web page so that I can place orders
- As a user I need a web page so that I can submit my order
- As a user I need a web page so that I can see list of products
- As a user I need a web page so that I can to see product details
- As a user I need a web page so that I can see my shopping list

As a < type of user >, I want < some goal > so that < some reason >.

##### Task

Create a React or more to completed US1
Create a React or more to completed US2

# Backlog Technical

### Task

- Create structure of date that we need to keep products
- Create structure of date that we need to store each order
- Create Json files regarding our data structures
- Design blue print of each page and component
-

## EPICS

## Features

## User Stories

## Task

<!-- -------------------------------------------------- -->

# Order Products

# Backlog Technical

### Setup DB

- As a frontend developer I need “ Fake data for products”
  - Create mock route for GET /api/products/data - Make some fake data
  - Create mock route for POST / api/products/data - post data to update products
  - Create mock route for POST / api/order/data - post data to store an order

### Search box component feature

- As a user, I want to be able to search for products name so that I can reach the specific product.
  - Search component
  - Load the search component in product list page to be able to search items.
  - Display two kinds of result to show, based on find item(partial list) or not find any item (empty view)

### Navigation feature

- As a user I want to be able to go back to the Home screen (List items) so that I can see all the products

### Products feature

- As a User I want to see all product in the first page.
  - Create a component to show a box to show product.
  - Create all products in a one page named Product List.
- As a User I want see the name, image of product and price for each product so I can click on it.
  - make an appropriate size for preview image.
- As a User I want click on 'Show More' button on each product so that the next page will show detail of product

### Detail feature

- As a User I want to see the product details like bigger image, title, description, price , availability and brand.
- As a User I need to add a product to my shopping list.
- As a User I want to increase or decrease number of products.

### Shopping list feature

- As a User I want to navigate to my shopping list
  - create an icon in the toolbar
- As a User I want to see my shopping list.
- As a User I want to be able to increase or decrease number of each item.
- As a User I want to be able to remove some of items from the shopping list.
- As a User I want to see the total price.

### Checkout feature

- As a User I want to submit my order.


### Comment feature (Phase 2)

- As a User I want comment for a product so that I can give my comment and read other comments for each product - Should validate for appropriate input text
