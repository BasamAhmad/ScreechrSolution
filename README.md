# Screechr Requirements

# Scenario

_screechr_ is going to be a multi-platform social media app that just happens to be a lot like Twitter; except you screech, not tweet. Revolutionary stuff.

The most important task is to implement the REST API system that will allow the client developers to setup a test environment and test &amp; demo their clients running on iOS, Android, and web. The team has agreed on the following constraints:

- For the purposes of this demo data does NOT need to be persisted between runs and in-memory data storage is fine
- The API is to be a RESTful web service
- The solution produced should run equally well on a Mac or Linux OS _(bonus points for supplying a buildable and runnable docker container!)_
- A script may be provided for running the solution, or the README.md file should contain step-by-step instructions
- If multiple containers are used in the solution, Docker Compose is to be used

Prior to your arrival, the CTO of _screechr_ decided that any (or all) of the following languages &amp; frameworks would be OK to use:

- Latest .net Core/ASP.net Core
- Latest Go

The CTO stated she doesn&#39;t care if the demo is a monolith or a collection of microservices so long as it can be easily deployed and run.

## Authentication

The CTO has informed you she hasn&#39;t decided about how authorization or authentication will work just yet but that you&#39;re to go ahead with using a simple token-based authentication mechanism that validates a &quot;secret&quot; token value for a given user passed in via the Authorization header.

## Authorization

At this point any user can call any endpoint. However, the following rules should be applied:

- A user may modify only their own profile
- A user may modify only their own screeches
- All users (including unauthenticated users) may view all screeches
- Authenticated users may view all profiles

## Object Models

For the purposes of the demo-ready API the _screechr_ team has agreed that the following object models will cover their needs when implemented at the database level. They&#39;re leaving it up to you to decide what and how these values are exposed at the API layer (including naming).

### User Profile

The profile of the user. Contains the following attributes:

- Id – positive integer, upper limit \&gt;10 billion, uniquely identifies the user in the system, cannot be null.
- User name – The public user name of the user. 80 characters, unique, cannot be null or all whitespace.
- First Name – The first name of the user. 100 characters, cannot be null or whitespace. ● Last Name – The last name of the user. 100 characters, cannot be null or whitespace.
- Secret token – String used to validate requests as coming from a specific user for this demo. 32 characters (single byte OK), unique and cannot be null.
- Profile Image - Just a URI to a profile image. Can be null.
- Date created – The date &amp; time the user was added to the system. Cannot be null.
- Date modified – The most recent date &amp; time the user was added to the system.

### Screech

Our version of a tweet – it allows 1024 characters!

A user profile may be related to none, one, or many screeches. Contains the following attributes:

- Id – positive integer, upper limit \&gt;1 quintillion, uniquely identifies the screech in the system, cannot be null.
- Content – The content of the screech. 1024 characters, may be all whitespace but cannot be null.
- Creator Id – The user id who created the screech. Not nullable.
- Date created – The date &amp; time the user was added to the system. Cannot be null.
- Date modified – The most recent date &amp; time the user was added to the system.

## Demo API Functionality

_screechr_ is just starting up and because of this your CTO and product manager have agreed on the following must-haves for a demo-ready API:

- API returns correct HTTP error codes for successes and failures, but no additional client messaging is required
- The API accepts and responds with only JSON
- Possible to retrieve a profile by its key
- Possible to update a profile picture by itself
- Possible to update an entire profile,
- Possible to return a paged list of screeches o Default sort order is creation date in descending order o Can be requested in ascending order by creation date o Can be filtered to return only screeches created by a specific user

o Default page size is 50, maximum is 500

- Possible to return an individual screech by its key
- Possible to create a new screech
- Possible to update the text of a screech
- Authorization &amp; authentication rules are applied correctly
- All dates &amp; times will use ISO 8601 format in the UTC time zone
- All character data should be handled as double-byte
- Unit tests are important - the CTO would like to see a few implemented



_Thanks!_
