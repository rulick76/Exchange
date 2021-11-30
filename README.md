

Default route
---------------

When running the project the browser will be opened at this address https://localhost:44360/Exchange

List of routes:
---------------
/Rates (https://localhost:44360/Exchange/rates) will get the latest/current state of assets.

/Rates?date=2020-12-24 will get the state of the assets for the given day (date format have to be yyyy-MM-dd).

/Rates?format=csv or /Rates?date=2020-12-24&format=csv will get the result in csv format (default is json if no format was supplied)

/statistics??date=2020-12-24 will get statistics (aggregetion of assets min,max,average) by given date

/statistics??date=2020-12-24&interval=7 will get statistics (aggregetion of assets min,max,average) for each date in the date interval
for statistics route as well the user can choose a csv format

Covered as well :

Error handeling.

Validations.

Async operations.

DI.

Logging.


Not covered :

Configuration handeling (baseurl,token etc).

Unit tests.
