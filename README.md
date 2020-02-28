# Dynamic Linq Predicates
Build predicates and execute query at runtime

## Introduction

Have you ever tried to provide your users with a way to dynamically build their own query to filter a list? If you ever tried, maybe you found it a little complicated. If you never tried, it could be tedious to do. But, with the help of LINQ, it does not need to be that hard (indeed, it could be even enjoyable).

## Description
Predicate Builder is a powerful LINQ expression that is mainly used when too many search filter parameters are used for querying data by writing dynamic query expression. We can write a query like Dynamic SQL.

## Background

### Scenario
Filter record for the employees having salary  > 10000

A typical Linq Predicate used will be 
```C#
x=> x.salary > 10000 
```

But suppose you want to provide your users a way to choose on which field and with what value  they want to filter records like below
![Image Here](2020-02-22_13h19_47.png?raw=true "Dynamic filter from user input")

### Solution

One way is to write predicate for each individual filter and checking each time user hits 

```C#
If (filter == "Name")
  result = employees.Where(x=> x.Name ==  {value});
Else If (filter == "Dev")
  result = employees.Where(x=> x.Dev == {value});
Else If (filter == "Age")
  result = employees.Where(x=> x.Age ==  {value});
Else If (filter == "Salary")
  result = employees.Where(x=> x.Salary ==  {value});
```

Another way is building  dynamic predicate based on user’s selection with less code
Here field name and filter value both will be provided by User , for each individual criteria no need to write separate code.

```C#
 var andCriteria = new List<Predicate<Employee>>();
 Expression<Func<Employee, bool>> predicate;
 string Fieldname = string.Empty, FieldValue = string.Empty;
```
```C#
 var type = t.GetProperty(Fieldname);
                    andCriteria.Add(c => Cast(type.GetValue(c),   type.PropertyType) == Cast(FieldValue, type.PropertyType));
                    
 predicate = c => andCriteria.All(pred => pred(c));

 result = employees.AsQueryable().Where(predicate).ToList();
```
You can use the same logic when you want to apply filters  on multiple fields and want to sort data.(Attached in demo)

Demo's output attached for reference 
![Image Here](2020-02-28_13h02_12.png?raw=true "Output Screen")
