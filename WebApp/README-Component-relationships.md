# React Components relationship 

```mermaid


graph TD;

App<-->SearchComponent;
App -->Navbar
App -->Showcase

subgraph Get-data-from-User
SearchComponent-->SearchDetails;
SearchComponent-->cityName;
SearchComponent-->SelectDate;
SearchComponent-->ButtonSearch;
SearchComponent-->CreateRequest;
end



CreateRequest-->MakeHttpRequestGet;


subgraph Show-data;
ShowResults-->ShowDetails
end


subgraph communication-To-Server
subgraph GiveRate
ShowResults-->MakeHttpRequestSet
MakeHttpRequestSet-->ShowResults
end



MakeHttpRequestSet
subgraph GetData

MakeHttpRequestGet-->ShowResults
MakeHttpRequestGet
end


end



subgraph Back Side


Db1[(Database2)];
WebAPI-.->Db1[(Database)];
MakeHttpRequestGet-->WebAPI
MakeHttpRequestSet-->WebAPI


end

````