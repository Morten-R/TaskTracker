Task Tracker CLI

A simple command-line task tracker written in C#.

Features

- Add tasks
- Update tasks
- Delete tasks
- Mark tasks as ToDo, InProgress or Done
- List all tasks
- Filter tasks by status
- JSON persistence

Running

This is an example of how to run it.


dotnet run add "Buy milk"

dotnet run add "Make dinner"

dotnet run update 1 "Buy oat milk"

dotnet run delete 1

dotnet run mark 2 Done

dotnet run list

dotnet run list done



If you're stuck and/or don't understand how it works, type "dotnet run help".