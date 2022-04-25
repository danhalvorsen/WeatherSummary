# React Components relationship 

```mermaid


graph TD;

App<-->Search;

App-->Resource-selection;

subgraph Get-data-from-User
Search-->SearchDetails;
Resource-selection;
end



App-->OpenAPI-Generated;

App--->ShowResults
subgraph Show-data;
ShowResults-->ShowDetails
end

subgraph communication-To-Server
OpenAPI
OpenAPI-.->Db1[(Database)];
subgraph GiveRate
ShowResults-->Make-Post-Request

end

Make-Post-Request-->OpenAPI
subgraph GetData
OpenAPI-Generated
end
OpenAPI-Generated-->OpenAPI

end


OpenAPI-.->Db1[(Database)];


````