//fxQueryAPI

let
  Result = (APIKey as text, RequestUrl as text, MaxPages as number) => 
    let

      //Depending on API the APIKey may need preceeding by something other than Bearer.
      apiKey = "Bearer " & APIKey,

      //Inner function that retrieves a single page of data, depending on API the ?cursor= and Headers may need adjusting.
      FnGetOnePage = (baseUrl, optional cursor) as record =>
        let
          url = if cursor = null then baseUrl else baseUrl & "?cursor=" & Number.ToText(cursor),
          Source = Json.Document(Web.Contents(url, [Headers=[Authorization = apiKey]])),
          hasMoreValue = try Source[has_more] otherwise null,
          cursorValue = try Source[cursor] otherwise null,
          res = [RawJSON = Source, HasMore = hasMoreValue, Cursor=cursorValue, Url = url]
        in
          res,

      //Loop through until there are no more pages.
      GeneratedList = List.Generate(
        ()=>[i=0, res = FnGetOnePage(RequestUrl)],
        each [i] < MaxPages and [res] <> null,
        each [i= [i]+1, res = if [res][HasMore] then FnGetOnePage(RequestUrl, [res][Cursor]) else null],
        each [res][RawJSON])
    in
      GeneratedList
in
    Result