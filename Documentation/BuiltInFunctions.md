

### NTypewriter



#### ActionFunctions

Method | Description | Returns
-------|-------------|---------
BodyParameter | Returns parameter that receives content sent to a webapi action in a request body.  |  `IParameter`
HttpMethod | Returns the http method used with a webapi action.            The http method is extracted from Http* or AcceptVerbs attribute or by naming convention if no attributes are specified.  |  `string`
Parameters | Returns parameters that receive content sent to a webapi action.  |  `IEnumerable<IParameter>`
ReturnType | Returns type that is returned from action unwrapped from Task and ActionResult generics  |  `IType`
Url | Returns the url for the Web API action based on route attributes (or the supplied convention route if no attributes are present).            Route parameters are converted to TypeScript string interpolation syntax by prefixing all parameters with $ e.g. ${id}.            Optional parameters are added as QueryString parameters for GET and HEAD requests.  |  `string`

#### ParametersFunctions

Method | Description | Returns
-------|-------------|---------
ToTypeScript |   |  `IEnumerable<string>`

#### StringFunctions

Method | Description | Returns
-------|-------------|---------
ToCamelCase | Converts text case to CamelCase  |  `string`
ToUpperFirst | Converts first letter of the given string to upper case  |  `string`
ToLowerFirst | Converts first letter of the given string to lower case  |  `string`

#### SymbolsFunctions

Method | Description | Returns
-------|-------------|---------
WhereNamespaceStartsWith | Filters symbols by the beginning of their namespace  |  `IEnumerable<ISymbolBase>`
WhereNamespaceEndsWith | Filters symbols by the end of their namespace  |  `IEnumerable<ISymbolBase>`
WhereNamespaceMatches | Filters symbols by regex pattern  |  `IEnumerable<ISymbolBase>`
WhereNameStartsWith | Filters symbols by the beginning of their name  |  `IEnumerable<ISymbolBase>`
WhereNameEndsWith | Filters symbols by the end of their name  |  `IEnumerable<ISymbolBase>`
WhereNameMatches | Filters symbols by a regex pattern  |  `IEnumerable<ISymbolBase>`
ThatHaveAttribute | Filters symbols by the presence of an attribute  |  `IEnumerable<ISymbolBase>`
ThatArePublic | Filters symbols by the public access modifier  |  `IEnumerable<ISymbolBase>`
ThatAreStatic | Filters symbols by the static modifier  |  `IEnumerable<ISymbolBase>`

#### TypeFunctions

Method | Description | Returns
-------|-------------|---------
AllReferencedTypes |   |  `IEnumerable<IType>`
ToTypeScriptDefault | The default value of the type.            (Dictionary types returns {}, enumerable types returns [],            boolean types returns false, numeric types returns 0, void returns void(0),            Guid types return empty guid string, Date types return new Date(0),            all other types return null)  |  `string`
ToTypeScriptType | Converts type name to typescript type name  |  `string`
Unwrap | Returns the first TypeArgument of a generic type or the type itself if it's not generic.  |  `IType`

#### TypesFunctions

Method | Description | Returns
-------|-------------|---------
ThatImplement | Filters types based on if a type implements given interface (directly or indirectly)  |  `IEnumerable<IType>`
ThatInheritFrom | Filters types based on if a type inherits directly from given type  |  `IEnumerable<IType>`

### Scriban


   
#### ArrayFunctions

Method | Description | Returns
-------|-------------|---------
Add | Adds a value to the input list.  |  `IEnumerable`
AddRange | Concatenates two lists.  |  `IEnumerable`
Compact | Removes any non-null values from the input list.  |  `IEnumerable`
Concat | Concatenates two lists.  |  `IEnumerable`
Cycle | Loops through a group of strings and outputs them in the order that they were passed as parameters. Each time cycle is called, the next string that was passed as a parameter is output.  |  `object`
Each | Applies the specified function to each element of the input.  |  `ScriptRange`
Filter | Filters the input list according the supplied filter function.  |  `ScriptRange`
First | Returns the first element of the input `list`.  |  `object`
InsertAt | Inserts a `value` at the specified index in the input `list`.  |  `IEnumerable`
Join | Joins the element of a list separated by a delimiter string and return the concatenated string.  |  `string`
Last | Returns the last element of the input `list`.  |  `object`
Limit | Returns a limited number of elments from the input list  |  `IEnumerable`
Map | Accepts an array element's attribute as a parameter and creates an array out of each array element's value.  |  `IEnumerable`
Offset | Returns the remaining of the list after the specified offset  |  `IEnumerable`
RemoveAt | Removes an element at the specified `index` from the input `list`  |  `IList`
Reverse | Reverses the input `list`  |  `IEnumerable`
Size | Returns the number of elements in the input `list`  |  `int`
Sort | Sorts the elements of the input `list` according to the value of each element or the value of the specified `member` of each element  |  `IEnumerable`
Uniq | Returns the unique elements of the input `list`.  |  `IEnumerable`
Contains | Returns if an `list` contains an specifique element  |  `bool`


   
   
   
   
#### DateTimeFunctions

Method | Description | Returns
-------|-------------|---------
Now | Returns a datetime object of the current time, including the hour, minutes, seconds and milliseconds.  |  `DateTime`
AddDays | Adds the specified number of days to the input date.  |  `DateTime`
AddMonths | Adds the specified number of months to the input date.  |  `DateTime`
AddYears | Adds the specified number of years to the input date.  |  `DateTime`
AddHours | Adds the specified number of hours to the input date.  |  `DateTime`
AddMinutes | Adds the specified number of minutes to the input date.  |  `DateTime`
AddSeconds | Adds the specified number of seconds to the input date.  |  `DateTime`
AddMilliseconds | Adds the specified number of milliseconds to the input date.  |  `DateTime`
Parse | Parses the specified input string to a date object.  |  `DateTime?`


   
#### HtmlFunctions

Method | Description | Returns
-------|-------------|---------
Strip | Removes any HTML tags from the input string  |  `string`
Escape | Escapes a HTML input string (replacing `&` by `&amp;`)  |  `string`
UrlEncode | Converts any URL-unsafe characters in a string into percent-encoded characters.  |  `string`
UrlEscape | Identifies all characters in a string that are not allowed in URLS, and replaces the characters with their escaped variants.  |  `string`


   
   
   
   
#### MathFunctions

Method | Description | Returns
-------|-------------|---------
Abs | Returns the absolute value of a specified number.  |  `object`
Ceil | Returns the smallest integer greater than or equal to the specified number.  |  `double`
DividedBy | Divides the specified value by another value. If the divisor is an integer, the result will            be floor to and converted back to an integer.  |  `object`
Floor | Returns the largest integer less than or equal to the specified number.  |  `double`
Format | Formats a number value with specified [.NET standard numeric format strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings)  |  `string`
IsNumber | Returns a boolean indicating if the input value is a number  |  `bool`
Minus | Substracts from the input value the `with` value  |  `object`
Modulo | Performs the modulo of the input value with the `with` value  |  `object`
Plus | Performs the addition of the input value with the `with` value  |  `object`
Round | Rounds a value to the nearest integer or to the specified number of fractional digits.  |  `double`
Times | Performs the multiplication of the input value with the `with` value  |  `object`


   
   
#### RegexFunctions

Method | Description | Returns
-------|-------------|---------
Escape | Escapes a minimal set of characters (`\`, `*`, `+`, `?`, `|`, `{`, `[`, `(`,`)`, `^`, `$`,`.`, `#`, and white space)            by replacing them with their escape codes.            This instructs the regular expression engine to interpret these characters literally rather than as metacharacters.  |  `string`
Match | Searches an input string for a substring that matches a regular expression pattern and returns an array with the match occurences.  |  `ScriptArray`
Matches | Searches an input string for multiple substrings that matches a regular expression pattern and returns an array with the match occurences.  |  `ScriptArray`
Replace | In a specified input string, replaces strings that match a regular expression pattern with a specified replacement string.  |  `string`
Split | Splits an input string into an array of substrings at the positions defined by a regular expression match.  |  `ScriptArray`
Unescape | Converts any escaped characters in the input string.  |  `string`


   
#### StringFunctions

Method | Description | Returns
-------|-------------|---------
Escape | Escapes a string with escape characters.  |  `string`
Append | Concatenates two strings  |  `string`
Capitalize | Converts the first character of the passed string to a upper case character.  |  `string`
Capitalizewords | Converts the first character of each word in the passed string to a upper case character.  |  `string`
Contains | Returns a boolean indicating whether the input string contains the specified string `value`.  |  `bool`
Empty | Returns a boolean indicating whether the input string is an empty string.  |  `bool`
Whitespace | Returns a boolean indicating whether the input string is empty or contains only whitespace characters.  |  `bool`
Downcase | Converts the string to lower case.  |  `string`
EndsWith | Returns a boolean indicating whether the input string ends with the specified string `value`.  |  `bool`
Handleize | Returns a url handle from the input string.  |  `string`
Literal | Return a string literal enclosed with double quotes of the input string.  |  `string`
LStrip | Removes any whitespace characters on the **left** side of the input string.  |  `string`
Pluralize | Outputs the singular or plural version of a string based on the value of a number.  |  `string`
Prepend | Concatenates two strings by placing the `by` string in from of the `text` string  |  `string`
Remove | Removes all occurrences of a substring from a string.  |  `string`
RemoveFirst | Removes the first occurrence of a substring from a string.  |  `string`
Replace | Replaces all occurrences of a string with a substring.  |  `string`
ReplaceFirst | Replaces the first occurrence of a string with a substring.  |  `string`
RStrip | Removes any whitespace characters on the **right** side of the input string.  |  `string`
Size | Returns the number of characters from the input string  |  `int`
Slice | The slice returns a substring, starting at the specified index. An optional second parameter can be passed to specify the length of the substring.            If no second parameter is given, a substring with the remaining characters will be returned.  |  `string`
Slice1 | The slice returns a substring, starting at the specified index. An optional second parameter can be passed to specify the length of the substring.            If no second parameter is given, a substring with the first character will be returned.  |  `string`
Split | The `split` function takes on a substring as a parameter.            The substring is used as a delimiter to divide a string into an array. You can output different parts of an array using `array` functions.  |  `IEnumerable`
StartsWith | Returns a boolean indicating whether the input string starts with the specified string `value`.  |  `bool`
Strip | Removes any whitespace characters on the **left** and **right** side of the input string.  |  `string`
StripNewlines | Removes any line breaks/newlines from a string.  |  `string`
ToInt | Converts a string to an integer  |  `object`
ToLong | Converts a string to a long 64 bit integer  |  `object`
ToFloat | Converts a string to a float  |  `object`
ToDouble | Converts a string to a double  |  `object`
Truncate | Truncates a string down to the number of characters passed as the first parameter.            An ellipsis (...) is appended to the truncated string and is included in the character count  |  `string`
Truncatewords | Truncates a string down to the number of words passed as the first parameter.            An ellipsis (...) is appended to the truncated string.  |  `string`
Upcase | Converts the string to uppercase  |  `string`
Md5 | Computes the `md5` hash of the input string  |  `string`
Sha1 | Computes the `sha1` hash of the input string  |  `string`
Sha256 | Computes the `sha256` hash of the input string  |  `string`
HmacSha1 | Converts a string into a SHA-1 hash using a hash message authentication code (HMAC). Pass the secret key for the message as a parameter to the function.  |  `string`
HmacSha256 | Converts a string into a SHA-256 hash using a hash message authentication code (HMAC). Pass the secret key for the message as a parameter to the function.  |  `string`
PadLeft | Pads a string with leading spaces to a specified total length.  |  `string`
PadRight | Pads a string with trailing spaces to a specified total length.  |  `string`
Base64Encode | Encodes a string to its Base64 representation.            Its character encoded will be UTF-8.  |  `string`
Base64Decode | Decodes a Base64-encoded string to a byte array.            The encoding of the bytes is assumed to be UTF-8.  |  `string`


   
#### TimeSpanFunctions

Method | Description | Returns
-------|-------------|---------
FromDays | Returns a timespan object that represents a `days` interval  |  `TimeSpan`
FromHours | Returns a timespan object that represents a `hours` interval  |  `TimeSpan`
FromMinutes | Returns a timespan object that represents a `minutes` interval  |  `TimeSpan`
FromSeconds | Returns a timespan object that represents a `seconds` interval  |  `TimeSpan`
FromMilliseconds | Returns a timespan object that represents a `milliseconds` interval  |  `TimeSpan`
Parse | Parses the specified input string into a timespan object.  |  `TimeSpan`



