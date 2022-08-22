
# Cymax.HomeAssignment

Code Challenge that exemplifies the consumption of 3 APIs exposed by different companies and the consumption of these in parallel through a Console Client.

### Solution Projects

- `Company1.API`
- `Company2.API`
- `Company3.API`
- `Cymax.Console.Client`
- `Cymax.UnitTest`

## Diagram

![alt text](https://github.com/glennperez/Cymax.HomeAssignment/blob/main/diagram.png?raw=true)

## APIs Definitions

#### Company 1 - (`Company1.API`)
- .Net Core 6 Platform
```http
  POST http://localhost:5161/deals
```
###### Request
```
{
  "contactAddress": "string",
  "warehouseAddress": "string",
  "packageDimensions": [
    23, 55, 10
  ]
}
```
###### Response
```
{
    "total": 3572
}
```
#### Company 2 - (`Company2.API`)
- .Net Core 6 Platform
```http
  POST http://localhost:5039/offers
```
###### Request
```
{
  "consignee": "string",
  "consignor": "string",
  "cartons": [
    4, 18, 32
  ]
}
```
###### Response
```
{
    "amount": 1349
}
```
#### Company 3 - (`Company3.API`)
- .Net Core 3.1 (for easier use of XML)
```http
  POST http://localhost:5000/bids
```

| Parameter | Type     | Value                       |
| :-------- | :------- | :-------------------------------- |
| `Accept`      | `Header` | `application/xml` |

###### Request
```
<?xml version='1.0' encoding='UTF-8'?>
<root>
    <source>string</source>
    <destination>string</destination>
    <packages>
        <package>23</package>
        <package>9</package>
    </packages>
</root>
```
###### Response
```
<xml xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
    <quote>3512</quote>
</xml>
```
## Usage

- 1- Make sure all APIs are running at the same time on the machine to be tested.
- 2- If possible try them 1 by 1 separately. **(you can also run the project `Cymax.UnitTest` To check the availability of the APIs)**
- 3- Run `Cymax.Console.Client` project.

If all apis are running the result is something like this:

![alt text](https://github.com/glennperez/Cymax.HomeAssignment/blob/main/console1.jpeg?raw=true)

If any one is not available, the client will indicate it: 

![alt text](https://github.com/glennperez/Cymax.HomeAssignment/blob/main/console2.jpeg?raw=true)

**Note! at least one of the APIs must be running.**

