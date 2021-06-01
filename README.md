# MarsRovers
*Leer esto en [Español](https://github.com/Axthal/MarsRovers/blob/master/README.es.md)*

A possible solution for one of the C# knowledge test called "Mars Rovers".

It is a console application that was created using .NET Core and C#. It does not use external libraries or third party software, only Regex, List and Linq.

Develop by Alejandro Cabrera Lagunes.

## Project Structure

It was developed trying to follow a well structured code base objective and best practice, so I created 3 classes (in adition to Program.cs) that contain the necessary methods.

###Model > Rover.cs
Base class with definitions for rover's properties, like position and instructions to be executed.

###Controller > RoverSet.cs
Class for rovers controll. It stores in memory the boundaries of the map and a list of rovers and, among its methods, have one that executes each one of it's rovers instructions. This method also checks detects if a rover has gone out of bounds because of the instructions and notify the user, showing the sequence of instructions that led the rover to it. 

###Services > InputHelper.cs
This class has 2 methods that help in requesting user input :
* *GetIntFromInput*: Query the user for some valid integer and returns it
* *GetValidInputByRegex*: Query the user for some valid text accordingly to a Regex pattern. Case Insensitive for default.

###Program.cs
Principal class that have the Main method and methods that display the main menu, process the set's configuration and execution.
