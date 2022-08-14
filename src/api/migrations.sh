#!/bin/bash

# create migration
 dotnet ef migrations add <name>
 
# remove migration
dotnet ef migrations remove

# upsert migrations
dotnet ef database update
